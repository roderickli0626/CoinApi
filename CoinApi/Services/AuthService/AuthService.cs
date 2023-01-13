using CoinApi.DB_Models;
using CoinApi.Helpers;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.EmailService;
using CoinApi.Services.LanguageService;
using CoinApi.Services.UserService;
using CoinApi.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private IConfiguration config;
        private IUserService userService;
        private ILanguageService langaugeService;
        private IEmailService emailService;
        public AuthService(IConfiguration config, IUserService userService, ILanguageService languageService, IEmailService emailService)
        {
            this.config = config;
            this.userService = userService;
            this.langaugeService = languageService;
            this.emailService = emailService;
        }

        public async Task<AuthenticatedResponse?> Login(LoginModel user)
        {
            tblUser db_user = await userService.GetByEmailAsync(user.email);

            if (db_user == null)
                return null;

            if (db_user.Password != Hasher.HashPassword(user.password))
                return null;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Issuer"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            var refreshToken = "";
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }

            return new AuthenticatedResponse
            {
                Token = tokenString,
                refreshToken = refreshToken,
                result = true,
                userId = db_user.UserID,
                languageNumber = db_user.LanguageNumber,
                deviceNumber = db_user.DeviceNumber,
                userName = db_user.FirstName + " " + db_user.LastName
            };
        }
        public async Task<ApiResponse> CheckLogin(LoginModel user)
        {
            tblUser db_user = await userService.GetByEmailAsync(user.email);
            if (db_user == null)
                return ApiErrorResponse("Please enter valid email address.");

            if (db_user.Password != PasswordHelper.Encrypt(user.password))
                return ApiErrorResponse("Please enter valid password.");

            if (!Convert.ToBoolean(db_user.IsEnableLogin) && db_user.tblCategory != null && (db_user.tblCategory.Name == "Therapeuten" || db_user.tblCategory.Name == "Händler"))
                return ApiErrorResponse("Your account is not approved yet. Please contact admin.");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Issuer"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            var refreshToken = "";
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }

            var authenticateData = new AuthenticatedResponse
            {
                Token = tokenString,
                refreshToken = refreshToken,
                result = true,
                userId = db_user.UserID,
                languageNumber = db_user.LanguageNumber,
                deviceNumber = db_user.DeviceNumber,
                userName = db_user.FirstName + " " + db_user.LastName,
                Email = db_user.Email,
                Category = db_user.tblCategory != null ? db_user.tblCategory.Name : ""
            };
            return ApiSuccessResponses(authenticateData, "Login successfully.");
        }

        public async Task<AuthenticatedResponse?> Register(RegisterModel model)
        {
            if (userService.GetBy(u => u.Email == model.email).Count > 0)
            {
                return null;
            }

            tblLanguage language = new tblLanguage()
            {
                description = model.language
            };
            tblLanguage insertedLanguage = langaugeService.Create(language);

            tblUser user = new tblUser()
            {
                FirstName = model.firstname,
                LastName = model.familyName,
                Email = model.email,
                Password = Hasher.HashPassword(model.password),
                DeviceNumber = model.serialNumber,
                LanguageNumber = insertedLanguage.languageNumber,
            };

            tblUser insertedUser = userService.Create(user);

            LoginModel loginModel = new LoginModel()
            {
                email = model.email,
                password = model.password
            };

            return await Login(loginModel);
        }
        public async Task<ApiResponse> SendResetPasswordRequest(string email)
        {
            var userEntity = await userService.GetByEmailAsync(email);
            if (userEntity != null)
            {
                string activationCode = RandomCodeGenerator.RandomString(5);
                var passwordResetLink = emailService.GetPasswordResetLink(PasswordHelper.Encrypt(userEntity.UserID.ToString()));
                var status = await emailService.SendPasswordResetEmail(email, passwordResetLink, userEntity.FirstName + " " + userEntity.SurName);
                if (status)
                    return ApiSuccessResponses(null, "Password reset link successfully sent on your registered email.");
                else
                    return ApiErrorResponse("Unable to send email.");
            }
            return ApiValidationResponse("Please enter valid email address.");
        }
    }
}

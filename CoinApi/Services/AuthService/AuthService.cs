using CoinApi.DB_Models;
using CoinApi.Helpers;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.UserService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CoinApi.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private IConfiguration config;
        private IUserService userService;
        public AuthService(IConfiguration config, IUserService userService)
        {
            this.config = config;
            this.userService = userService;
        }

        public AuthenticatedResponse? Login(LoginModel user)
        {
            tblUser db_user = userService.GetByEmail(user.email);

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

            return new AuthenticatedResponse { 
                Token = tokenString, 
                refreshToken = refreshToken, 
                result = true, 
                userId = db_user.UserID 
            };
        }

        public AuthenticatedResponse? Register(RegisterModel model)
        {
            if (userService.GetBy(u => u.Email == model.email).Count > 0)
            {
                return null;
            }

            tblUser user = new tblUser()
            {
                FirstName = model.firstname,
                LastName = model.familyName,
                Email = model.email,
                Password = Hasher.HashPassword(model.password),
                DeviceNumber = model.serialNumber,
                Language = model.language,
            };

            tblUser insertedUser = userService.Create(user);

            LoginModel loginModel = new LoginModel()
            {
                email = model.email,
                password = model.password
            };

            return Login(loginModel);
        }
    }
}

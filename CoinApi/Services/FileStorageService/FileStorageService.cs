namespace CoinApi.Services.FileStorageService
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;
        public FileStorageService(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        public string GetHostRootUrl()
        {
            var currentRequest = _httpContextAccessor.HttpContext.Request;
            var hostRootUrl = currentRequest.Scheme.ToString() + "://" + currentRequest.Host.Value + "/";
            return hostRootUrl;
        }
        public string GetFullImageUrl(string imageDirectoryName, string itemImageUri)
        {
            if (itemImageUri != null)
            {
                var hostRootUrl = GetHostRootUrl();
                return GetFullImageUrl(hostRootUrl, imageDirectoryName, itemImageUri);
            }
            return null;
        }

        public string GetFullImageUrl(string hostRootUrl, string imageDirectoryName, string itemImageUri)
        {
            if (itemImageUri != null)
            {
                return string.Concat(hostRootUrl, imageDirectoryName, "/", itemImageUri);
            }
            return null;
        }
        public string SaveFileFromBase64String(string directoryPath, string base64String, string fileExtension)
        {
            try
            {
                var subDirectory = $"{_environment.WebRootPath}\\{directoryPath}\\";
                if (!Directory.Exists(subDirectory))
                    Directory.CreateDirectory(subDirectory);

                var mainFileName = $"{Guid.NewGuid()}.{(fileExtension == ".csv" ? "csv": GetFileExtension(base64String))}";
                var mainFilePath = subDirectory + mainFileName;
                var mainConvertBytes = Convert.FromBase64String(base64String);
                var saveFile = new FileStream(mainFilePath, FileMode.Create);
                saveFile.Write(mainConvertBytes, 0, mainConvertBytes.Length);
                saveFile.Flush();

                return mainFileName;
            }
            catch { }
            return null;
        }

        public string SaveFileFromByteArray(string directoryPath, byte[] byteArr, string fileExtension)
        {
            try
            {
                var subDirectory = $"{_environment.WebRootPath}\\{directoryPath}\\";

                if (!Directory.Exists(subDirectory))
                    Directory.CreateDirectory(subDirectory);

                var mainFileName = $"{Guid.NewGuid()}.{fileExtension}";
                string path = subDirectory + mainFileName;
                File.WriteAllBytes(path, byteArr);

                return mainFileName;
            }
            catch { }
            return null;
        }

        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                case "UKLGR":
                    return "wav";
                case "fb208":
                    return "csv";
                default:
                    return string.Empty;
            }
        }

        public bool DeleteFile(string directoryPath, string filename)
        {
            try
            {
                var subDirectory = $"{_environment.WebRootPath}\\{directoryPath}\\";
                var filePath = subDirectory + filename;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                return true;
            }
            catch { }
            return false;
        }
    }
}

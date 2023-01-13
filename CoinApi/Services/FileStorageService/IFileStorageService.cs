namespace CoinApi.Services.FileStorageService
{
    public interface IFileStorageService
    {
        string GetHostRootUrl();
        string GetFullImageUrl(string imageDirectoryName, string itemImageUri);
        string GetFullImageUrl(string hostRootUrl, string imageDirectoryName, string itemImageUri);
        string SaveFileFromBase64String(string directoryPath, string base64String, string fileExtension);
        bool DeleteFile(string directoryPath, string filename);
        string SaveFileFromByteArray(string directoryPath, byte[] byteArr, string fileExtension);
    }
}

namespace Electronic_Application.Contracts.Infastructure
{
    public interface IBlobStorageService
    {
        ///Task<string> UploadFileToBlobStroge(string blobContainer, IFormFile file, string fullFileName = null);
        Task<string> UploadStreamToBlobStroge(string blobContainer, MemoryStream stream, string fileType);
        Task<byte[]> DownloadFileFromBlobStroge(string blobContainer, string fileName);
        Task<bool> DeleteFileFromBlobStroge(string blobContainer, string fileName);
        List<string> GetAllFilesFromBlobStroge(string blobContainer, string prefix = null);
        Task<bool> CopyFiles(string destinationBlobContainer, string destinationFolderWithFileName, Uri sourceUri);
        Task<bool> CreateBlobContainer(string blobContainer);
        Task<bool> DeleteBlobContainer(string blobContainer);
        Task UnZipFolder(string blobContainer, string fileName, string extractContainer);
        Task UploadFileToBlobStrogeUsingStream(string blobContainer, MemoryStream stream, string fileName);
    }
}

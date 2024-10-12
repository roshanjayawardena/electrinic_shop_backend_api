using Electronic_Application.Contracts.Infastructure;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Electronic_Infastructure.FireBaseStorage
{
    public class FirebaseStorageService: IFirebaseStorageService
    {
        private readonly StorageClient _storageClient;
        private const string BucketName = "electronic-shop-9fe85.appspot.com";
        public FirebaseStorageService(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }
        public async Task<Uri> UploadFile(IFormFile file)
        {
            var randomGuid = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var blob = await _storageClient.UploadObjectAsync(BucketName,
                $"eshop/{randomGuid}{extension}", file.ContentType, stream);
            var photoUri = new Uri(blob.MediaLink);          
            return photoUri;
        }


        public async Task<bool> CheckExistOrNot(string fileUrl) 
        {
            string imageWithFolder = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
            string imageName = Uri.UnescapeDataString(imageWithFolder).Split('/')[1];           
            var imageObject = await _storageClient.GetObjectAsync(BucketName, $"eshop/{imageName}");
            return imageObject != null ? true : false;
        }

        public async Task DeleteBlob(string fileUrl)
        {
            string imageWithFolder = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
            string imageName = Uri.UnescapeDataString(imageWithFolder).Split('/')[1];
            await _storageClient.DeleteObjectAsync(BucketName, $"eshop/{imageName}");            
        }
    }
}

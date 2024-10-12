using Microsoft.AspNetCore.Http;

namespace Electronic_Application.Contracts.Infastructure
{
    public interface IFirebaseStorageService
    {
        Task<Uri> UploadFile(IFormFile file);
        Task<bool> CheckExistOrNot(string fileUrl);
        Task DeleteBlob(string fileUrl);
    }
}

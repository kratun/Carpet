namespace Carpet.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);

        Task<string> UploadPdfBytesAsync(byte[] pdfBytes, string fileName);
    }
}

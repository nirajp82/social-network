using Microsoft.AspNetCore.Http;
using SocialNetwork.Dto;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhotoAsync(IFormFile formFile);
        Task DeletePhotoAsync(string publicId);
    }   
}

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using SocialNetwork.Dto;
using SocialNetwork.Nucleus;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure
{
    public class PhotoAccessor : IPhotoAccessor
    {
        #region Members
        private readonly InfrastructureConfigSettings _infrastructureConfig;
        #endregion


        #region Constructors
        public PhotoAccessor(InfrastructureConfigSettings infrastructureConfig)
        {
            _infrastructureConfig = infrastructureConfig;
        }
        #endregion


        #region Public Methods
        public async Task<PhotoUploadResult> AddPhotoAsync(IFormFile formFile)
        {
            PhotoUploadResult result = new PhotoUploadResult
            {
                PublicId = Guid.NewGuid().ToString(),//$"{Guid.NewGuid()}.{Path.GetExtension(formFile.FileName)}"
            };
            CloudBlockBlob blob = GetBlockBlobReference(result.PublicId);

            using (Stream fs = formFile.OpenReadStream())
                await blob.UploadFromStreamAsync(fs);

            result.Url = GetUri(result.PublicId);
            return result;
        }

        public async Task DeletePhotoAsync(string publicId)
        {
            CloudBlockBlob blob = GetBlockBlobReference(publicId);
            await blob.DeleteAsync();
        }
        #endregion


        #region Private Methods
        private string GetUri(string publicId)
        {
            CloudBlockBlob blob = GetBlockBlobReference(publicId);
            string signature = blob.GetSharedAccessSignature(CreateSharedAccessPolicy());
            return $"{_infrastructureConfig.UserImageContainerPath}/{publicId}{signature}";
        }

        private CloudBlockBlob GetBlockBlobReference(string publicId)
        {
            CloudBlobContainer container = GetBlobContainerReference();
            CloudBlockBlob blob = container.GetBlockBlobReference(publicId);
            return blob;
        }

        private CloudBlobContainer GetBlobContainerReference()
        {
            StorageCredentials credentials = new StorageCredentials(_infrastructureConfig.AccountName, _infrastructureConfig.AccountKey);
            CloudBlobClient blobClient = new CloudBlobClient(new Uri(_infrastructureConfig.BlobAccountBaseUri), credentials);
            return blobClient.GetContainerReference(_infrastructureConfig.UserImageContainerName);
        }

        private SharedAccessBlobPolicy CreateSharedAccessPolicy()
        {
            SharedAccessBlobPolicy blobPolicy = new SharedAccessBlobPolicy()
            {
                // When the start time for the SAS is omitted, the start time is assumed to be the time when the storage service receives the request.
                // Omitting the start time for a SAS that is effective immediately helps to avoid clock skew.
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(2),
                Permissions = SharedAccessBlobPermissions.Read
            };
            return blobPolicy;
        }
        #endregion
    }
}

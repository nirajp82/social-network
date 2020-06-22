using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using SocialNetwork.Dto;
using SocialNetwork.Nucleus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        public async Task<PhotoDto> AddPhotoAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            Guid id = Guid.NewGuid();
            PhotoDto result = new PhotoDto
            {
                Id = id,
                CloudFileName = $"{id}{Path.GetExtension(formFile.FileName)}"
            };
            CloudBlockBlob blob = GetBlockBlobReference(result.CloudFileName.ToString());

            using (Stream fs = formFile.OpenReadStream())
                await blob.UploadFromStreamAsync(fs, cancellationToken);

            result.Url = PreparePhotoUrl(blob.Name);
            return result;
        }

        public async Task DeletePhotoAsync(string cloudFileName, CancellationToken cancellationToken)
        {
            CloudBlockBlob blob = GetBlockBlobReference(cloudFileName);
            await blob.DeleteAsync(cancellationToken);
        }

        public IEnumerable<string> PreparePhotosUrl(IEnumerable<string> photosCloudName)
        {
            ICollection<string> urlList = new List<string>();
            SharedAccessBlobPolicy policy = CreateSharedAccessPolicy();
            foreach (var cloudFileName in photosCloudName)
            {
                if (!string.IsNullOrWhiteSpace(cloudFileName))
                {
                    CloudBlockBlob blob = GetBlockBlobReference(cloudFileName);
                    string signature = blob.GetSharedAccessSignature(policy);
                    string url = $"{_infrastructureConfig.UserImageContainerPath}/{cloudFileName}{signature}";
                    urlList.Add(url);
                }
            }
            return urlList;
        }

        public string PreparePhotoUrl(string cloudFileName)
        {
            if (!string.IsNullOrWhiteSpace(cloudFileName))
            {
                CloudBlockBlob blob = GetBlockBlobReference(cloudFileName);
                string signature = blob.GetSharedAccessSignature(CreateSharedAccessPolicy());
                return $"{_infrastructureConfig.UserImageContainerPath}/{cloudFileName}{signature}";
            }
            return cloudFileName;
        }
        #endregion


        #region Private Methods
        private CloudBlockBlob GetBlockBlobReference(string cloudFileName)
        {
            CloudBlobContainer container = GetBlobContainerReference();
            CloudBlockBlob blob = container.GetBlockBlobReference(cloudFileName);
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

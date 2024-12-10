using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos
{
    public class S3Service
    {
        private readonly string _bucketName = "bucket-coruja";
        private readonly IAmazonS3 _s3Client;

        public S3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _s3Client.ListObjectsV2Async(new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    MaxKeys = 1
                });

                return response.S3Objects.Count > 0 || response.KeyCount >= 0;
            }
            catch (AmazonS3Exception e)
            {
                // Log the exception
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<string>> ListObjectsAsync()
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = _bucketName
                };
                var response = await _s3Client.ListObjectsV2Async(request);

                var objectKeys = new List<string>();
                foreach (var entry in response.S3Objects)
                {
                    objectKeys.Add(entry.Key);
                }

                return objectKeys;
            }
            catch (AmazonS3Exception e)
            {
                // Log the exception
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<string> UploadImageAsync(string filePath, string keyName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_s3Client);

                await fileTransferUtility.UploadAsync(filePath, _bucketName, keyName);

                return $"https://{_bucketName}.s3.amazonaws.com/{keyName}";
            }
            catch (AmazonS3Exception e)
            {
                // Log the exception
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string keyName, string contentType)
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName,
                    InputStream = imageStream,
                    ContentType = contentType,
                    //CannedACL = S3CannedACL.PublicRead
                };

                var response = await _s3Client.PutObjectAsync(request);

                return $"https://{_bucketName}.s3.amazonaws.com/{keyName}";
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error uploading image: {e.Message}");
                throw;
            }



        }
        public async Task<List<S3Object>> ListObjectsAsync(string prefix)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = prefix
            };

            var response = await _s3Client.ListObjectsV2Async(request);

            return response.S3Objects;
        }

        public string GetPreSignedURL(string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.Now.AddHours(1) // URL expira em 1 hora
            };

            return _s3Client.GetPreSignedURL(request);
        }

    }
}

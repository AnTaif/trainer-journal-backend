using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using TrainerJournal.Application.Services;
using TrainerJournal.Domain.Options;

namespace TrainerJournal.Infrastructure.Services;

public class S3FileStorage(
    IOptions<S3Options> s3Options,
    IAmazonS3 s3Client) : IFileStorage
{
    public async Task<string> UploadAsync(Stream fileStream, string destinationDirectory, string destinationFile)
    {
        await EnsureBucketExistsAsync(destinationDirectory.Split("/")[0]);
        
        var request = new TransferUtilityUploadRequest
        {
            BucketName = destinationDirectory,
            Key = destinationFile,
            InputStream = fileStream
        };

        var transferUtility = new TransferUtility(s3Client);
        await transferUtility.UploadAsync(request);

        return $"{s3Options.Value.StorageUrl}/{destinationDirectory}/{destinationFile}";
    }
    
    private async Task EnsureBucketExistsAsync(string bucketName)
    {
        try
        {
            var response = await s3Client.ListBucketsAsync();
            var exists = response.Buckets.Exists(b => b.BucketName == bucketName);
            if (!exists)
            {
                await s3Client.PutBucketAsync(new PutBucketRequest { BucketName = bucketName });
            }
        }
        catch
        {
            await s3Client.PutBucketAsync(new PutBucketRequest { BucketName = bucketName });
        }
    }
}
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using TrainerJournal.Application.Services;
using TrainerJournal.Domain.Options;

namespace TrainerJournal.Infrastructure.Services;

public class S3FileStorage : IFileStorage
{

    private readonly S3Options s3Options;
    private readonly AmazonS3Client s3Client;
    
    public S3FileStorage(IOptions<S3Options> options)
    {
        s3Options = options.Value;

        var s3Config = new AmazonS3Config
        {
            ServiceURL = s3Options.StorageUrl,
            ForcePathStyle = true
        };

        s3Client = new AmazonS3Client(s3Options.AccessKey, s3Options.SecretKey, s3Config);
    }
    
    public async Task<string> UploadAsync(Stream fileStream, string destinationDirectory, string destinationFile)
    {
        await EnsureBucketExistsAsync(destinationDirectory.Split("/")[0]);
        
        var request = new TransferUtilityUploadRequest
        {
            BucketName = destinationDirectory,
            Key = destinationFile,
            InputStream = fileStream,
            
        };

        var transferUtility = new TransferUtility(s3Client);
        await transferUtility.UploadAsync(request);

        return $"{s3Options.StorageUrl}/{destinationDirectory}/{destinationFile}";
    }

    public void Delete(string destinationDirectory, string destinationFile)
    {
        throw new NotImplementedException();
    }

    private async Task EnsureBucketExistsAsync(string bucketName)
    {
        try
        {
            var response = await s3Client.ListBucketsAsync();
            var exists = response.Buckets.Exists(b => b.BucketName == bucketName);
            if (!exists)
            {
                await PutAnonymousBucketAsync(bucketName);
            }
        }
        catch
        {
            await PutAnonymousBucketAsync(bucketName);
        }
    }

    private async Task PutAnonymousBucketAsync(string bucketName)
    {
        await s3Client.PutBucketAsync(new PutBucketRequest { BucketName = bucketName });
        
        var policy = $@"
        {{
            ""Version"": ""2012-10-17"",
            ""Statement"": [
                {{
                    ""Effect"": ""Allow"",
                    ""Principal"": ""*"",
                    ""Action"": ""s3:GetObject"",
                    ""Resource"": ""arn:aws:s3:::{bucketName}/*""
                }}
            ]
        }}";

        // Устанавливаем политику
        await s3Client.PutBucketPolicyAsync(new PutBucketPolicyRequest
        {
            BucketName = bucketName,
            Policy = policy
        });
    }
}
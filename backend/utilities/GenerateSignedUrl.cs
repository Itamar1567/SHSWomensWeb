
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
public class GenerateSignedUrl
{
    private readonly ILogger<GenerateSignedUrl> _logger;

    public GenerateSignedUrl(ILogger<GenerateSignedUrl> logger)
    {
        _logger = logger;
    }

    public string GenerateV4SignedUrl(string fileName = "your-object-name", string fileType = "text/plain", string bucketName = "your-bucket-name")
    {

        try
        {
            UrlSigner urlSigner = UrlSigner.FromCredential(GoogleCredential.GetApplicationDefault());

            var contentHeaders = new Dictionary<string, IEnumerable<string>>
        {
            { "Content-Type", new[] { fileType } }
        };

            // V4 is the default signing version.
            UrlSigner.Options options = UrlSigner.Options.FromDuration(TimeSpan.FromHours(1));

            UrlSigner.RequestTemplate template = UrlSigner.RequestTemplate
                .FromBucket(bucketName)
                .WithObjectName(fileName)
                .WithHttpMethod(HttpMethod.Put)
                .WithContentHeaders(contentHeaders);

            string url = urlSigner.Sign(template, options);
            _logger.LogInformation("Generated PUT signed URL for file: {FileName} in bucket: {BucketName}", fileName, bucketName);
            return url;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error generating signed URL for file: {FileName}", fileName);
            throw new Exception("Failed to generate a signed URL request.");
        }
    }
}
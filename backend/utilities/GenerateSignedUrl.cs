
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Net.Http;
public class GenerateSignedUrl
{
    public string GenerateV4SignedUrl(string objectName = "your-object-name")
    {

        try
        {
            string bucketName = "shs_newsletter_images";

            UrlSigner urlSigner = UrlSigner.FromCredential(GoogleCredential.GetApplicationDefault());

            var contentHeaders = new Dictionary<string, IEnumerable<string>>
        {
            { "Content-Type", new[] { "text/plain" } }
        };

            // V4 is the default signing version.
            UrlSigner.Options options = UrlSigner.Options.FromDuration(TimeSpan.FromHours(1));

            UrlSigner.RequestTemplate template = UrlSigner.RequestTemplate
                .FromBucket(bucketName)
                .WithObjectName(objectName)
                .WithHttpMethod(HttpMethod.Put)
                .WithContentHeaders(contentHeaders);

            string url = urlSigner.Sign(template, options);
            Console.WriteLine("Generated PUT signed URL:");
            Console.WriteLine(url);
            Console.WriteLine("You can use this URL with any user agent, for example:");
            Console.WriteLine($"curl -X PUT -H 'Content-Type: text/plain' --upload-file my-file '{url}'");
            return url;
        }
        catch
        {
            Console.WriteLine("Error generating signed URL.");
            return null;
        }
    }
}
using System.Security;
using Microsoft.AspNetCore.Mvc;

public class SignedUrlRequest
{
    public required string name { get; set; }
    public required string type { get; set; }

    public required long size { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class CloudStorageController : ControllerBase
{
    private const long maxFileSize = 5 * 1024 * 1024; // 5 MB
     GenerateSignedUrl _generator;

    public CloudStorageController(GenerateSignedUrl generator)
    {
        _generator = generator;
    } 

    [HttpPost("generate-signed-url")]
    public IActionResult GenerateSignedUrl([FromBody] SignedUrlRequest file)
    {   
        Console.WriteLine("File name: " + file.name + ", File type: " + file.type);
        if(file.type != "image/jpeg" && file.type != "image/png")
        {
            return StatusCode(400, new { message = "Invalid file type. Only JPEG and PNG are allowed." });
        }

        if(file.size > maxFileSize || file.size <= 0)
        {
            return StatusCode(400, new { message = "Invalid file size. Files must be between 1 byte and 5 MB." });
        }

        var url = _generator.GenerateV4SignedUrl(file.name, file.type, "shs_newsletter_images");
        if (url != null)
        {
            return Ok(new { message = "Signed URL generated successfully.", signedUrl = url });
        }
        else
        {
            return StatusCode(500, new { message = "Error generating signed URL." });
        }
    }
}
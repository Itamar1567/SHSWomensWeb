using Microsoft.AspNetCore.Mvc;

public class SignedUrlRequest
{
    public required string name { get; set; }
    public required string type { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class CloudStorageController : ControllerBase
{

     GenerateSignedUrl _generator;

    public CloudStorageController(GenerateSignedUrl generator)
    {
        _generator = generator;
    } 

    [HttpPost("generate-signed-url")]
    public IActionResult GenerateSignedUrl([FromBody] SignedUrlRequest file)
    {   
        Console.WriteLine("File name: " + file.name + ", File type: " + file.type);
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
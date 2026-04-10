using Microsoft.AspNetCore.Mvc;

public class SignedUrlRequest
{
    public required string ObjectName { get; set; }
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
    public IActionResult GenerateSignedUrl([FromBody] SignedUrlRequest objectName)
    {
        var url = _generator.GenerateV4SignedUrl(objectName.ObjectName);
        if (url != null)
        {
            return Ok(new { signedUrl = url });
        }
        else
        {
            return StatusCode(500, "Error generating signed URL.");
        }
    }
}
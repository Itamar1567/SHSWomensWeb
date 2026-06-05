using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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
    private readonly GenerateSignedUrl _generator;
    private readonly DatabaseRepository _db;
    private readonly ILogger<CloudStorageController> _logger;

    public CloudStorageController(GenerateSignedUrl generator, DatabaseRepository db, ILogger<CloudStorageController> logger)
    {
        _db = db;
        _generator = generator;
        _logger = logger;
    }

    [EnableRateLimiting("authenticated_rate")]
    [Authorize]
    [HttpPost("generate-signed-url")]
    public async Task<IActionResult> GenerateSignedUrl([FromBody] SignedUrlRequest file)
    {
        _logger.LogInformation("Generate signed URL requested for file type: {FileType}", file.type);

        if (file != null)
        {

            if(file.name == null)
            {
                return StatusCode(400, new { message = "Please provide a name to the image." });
            }

            if (file.type != "image/jpeg" && file.type != "image/png")
            {
                return StatusCode(400, new { message = "Invalid file type. Only JPEG and PNG are allowed." });
            }

            if (file.size > maxFileSize || file.size <= 0)
            {
                return StatusCode(400, new { message = "Invalid file size. Files must be between 1 byte and 5 MB." });
            }

            //If image not a duplicate insert into images table
            if (!await _db.IsDuplicateImage(file.name))
            {
                //Was unable to insert
                if (!await _db.InsertImagePathToImageTable(file.name))
                {
                    return StatusCode(500, new { message = "Encountered an Error inseting the image into the database." });
                }

            }
            else
            {
                return StatusCode(400, new { message = "Image already exists. Added succesfully" });
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

        return Ok(new {message = "No signed url required"});

    }
}
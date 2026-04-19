<<<<<<< HEAD
=======
using System.Data.Common;
using System.Security;
>>>>>>> ddf3c3d (Disallow duplicate images in the Google Cloud Storage, Create Unique Image database table)
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
    DatabaseRepository _db;

    public CloudStorageController(GenerateSignedUrl generator, DatabaseRepository db)
    {
        _db = db;
        _generator = generator;
<<<<<<< HEAD

    } 

    [HttpPost("generate-signed-url")]
    public IActionResult GenerateSignedUrl([FromBody] SignedUrlRequest file)
    {           
=======
    }

    [HttpPost("generate-signed-url")]
    public async Task<IActionResult> GenerateSignedUrl([FromBody] SignedUrlRequest file)
    {
>>>>>>> ddf3c3d (Disallow duplicate images in the Google Cloud Storage, Create Unique Image database table)
        Console.WriteLine("File name: " + file.name + ", File type: " + file.type);

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

        return StatusCode(400, new {message = "No signed url required"});

    }
}
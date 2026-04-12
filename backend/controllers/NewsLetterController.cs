using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.Attr;
using ZstdSharp.Unsafe;

[ApiController]
[Route("api/[controller]")]
public class NewsLetterController : ControllerBase
{
    private readonly DatabaseRepository _db;

    public NewsLetterController(DatabaseRepository db)
    {
        _db = db;
    }



    [HttpGet]
    public async Task<ActionResult> GetNewsLetters()
    {
        var data = await _db.GetNewsletters();
        GetNewsLetterDTO[] newsletters = data.ToArray();
        return Ok(new { newsletters });
    }

    [HttpPost]
    public async Task<ActionResult> CreateNewsLetter([FromBody] CreateNewsLetterDTO newsletter)
    {
        try
        {

            if (await _db.IsDuplicateTitle(newsletter.title.ToLower()))
            {
                return StatusCode(400, new { message = "Newsletter with this title already exists." });
            }

            if (newsletter.image_path != null && await _db.IsDuplicateImage(newsletter.image_path))
            {
                Console.WriteLine(newsletter.image_path + " is a duplicate image path.");
                return StatusCode(400, new { message = "Newsletter with this image path already exists." });
            }

            Newsletters newNewsletter = new Newsletters
            {
                title = newsletter.title,
                slug = newsletter.slug,
                author = newsletter.author,
                image_path = newsletter.image_path,
                short_description = newsletter.short_description,
                story_text = newsletter.story_text
            };

            if (await _db.CreateNewsLetter(newNewsletter))
            {
                return Ok(new { message = "Newsletter created successfully" });
            }

            else
            {
                return StatusCode(500, new { message = "Failed to create newsletter" });
            }
        }
        catch
        {
            return StatusCode(500, new { message = "Failed to create newsletter" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNewsLetter(int id)
    {
        try
        {
            string data = await _db.DeleteNewsLetter(id);
            return Ok(new { message = data });
        }catch(Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }

    }

}
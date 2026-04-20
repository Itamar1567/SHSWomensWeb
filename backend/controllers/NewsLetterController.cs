using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NewsLetterController : ControllerBase
{
    private readonly DatabaseRepository _db;
    private readonly FrontendActions _frontendActions;
    public NewsLetterController(DatabaseRepository db, FrontendActions frontendActions)
    {
        _db = db;
        _frontendActions = frontendActions;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetNewsletterById(int id)
    {
        try
        {
            var editableNewsletter = await _db.GetNewsletterByIdFromDatabase(id);
            return Ok(new {editableNewsletter});
        }
        catch(Exception ex)
        {
            Console.WriteLine("Failed to get newsletter ", ex);
            return StatusCode(400, new {message = "Failed to get newsletter: " + ex});
        }
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
                await _frontendActions.RedeployMainWeb();
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
            await _frontendActions.RedeployMainWeb();
            return Ok(new { message = data });
        }catch(Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }

    }

}
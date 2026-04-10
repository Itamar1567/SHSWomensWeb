using Microsoft.AspNetCore.Mvc;

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
        Newsletters[] newsletters = data.ToArray();
        return Ok(new {newsletters});
    }

    [HttpPost]
    public async Task<ActionResult> CreateNewsLetter([FromBody] CreateNewsLetterDTO newsletter)
    {
        try
        {
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
                return Ok(new {message = "Newsletter created successfully"});
            }
            else
            {
                return StatusCode(500, new {message = "Failed to create newsletter"});
            }
        }
        catch
        {
            return StatusCode(500, new {message = "Failed to create newsletter"});
        }
    }
}
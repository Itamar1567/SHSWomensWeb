using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/[controller]")]
public class NewsLetterController : ControllerBase
{
    private readonly DatabaseRepository _db;
    private readonly ILogger<NewsLetterController> _logger;

    public NewsLetterController(DatabaseRepository db, FrontendActions frontendActions, ILogger<NewsLetterController> logger)
    {
        _db = db;
        _logger = logger;
    }                                                   

    [EnableRateLimiting("public_rate")]
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
            _logger.LogError(ex, "Error retrieving newsletter with id: {NewsLetterId}", id);
            return StatusCode(500, new {message = "An error occurred while retrieving the newsletter"});
        }
    }

    [EnableRateLimiting("authenticated_rate")]
    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ActionResult> OverrideNewsletterById([FromBody] EditNewsletterDTO editedNewsletter)
    {
        try
        {

            if(await _db.IsDuplicateTitle(editedNewsletter.title, editedNewsletter.id))
            {
                return StatusCode(400, new {message="A newsletter with this title already exists"});    
            }

            if(await _db.OverrideNewsletterByIdFromDatabase(editedNewsletter))
            {
                return Ok(new {message="Newsletter succesfully edited"});
            }
            else
            {
                return StatusCode(400, new {message="Failed to edit newsletter"});
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error editing newsletter with id: {NewsLetterId}", editedNewsletter.id);
            return StatusCode(500, new {message = "An error occurred while editing the newsletter"});
        }
    }


    [EnableRateLimiting("public_rate")]
    [HttpGet]
    public async Task<ActionResult> GetNewsLetters()
    {
        var data = await _db.GetNewsletters();
        GetNewsLetterDTO[] newsletters = data.ToArray();
        return Ok(new { newsletters });
    }

    [EnableRateLimiting("authenticated_rate")]
    [Authorize]
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

    [EnableRateLimiting("authenticated_rate")]
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNewsLetter(int id)
    {
        try
        {
            string data = await _db.DeleteNewsLetter(id);
            return Ok(new { message = data });
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Error deleting newsletter with id: {NewsLetterId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the newsletter" });
        }

    }

}
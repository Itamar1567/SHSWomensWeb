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
}
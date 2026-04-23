
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private DatabaseRepository _db;

    public TestController(DatabaseRepository db)
    {
        _db = db;
    }

    [HttpGet("hello")]
    public ActionResult GetHello()
    {
        return Ok(new {message = "Hello World"});
    }

    [Authorize]
    [HttpGet("database")]
    public ActionResult GetDatabase()
    {
        if (_db.TestConnection())
        {
            return Ok(new {message = "Database connection successful"});
        }
        else
        {
            return StatusCode(500, new {message = "Database connection failed"});
        }
    }
}

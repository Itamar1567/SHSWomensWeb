
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private ILogger<TestController> _logger;
    private DatabaseRepository _db;

    public TestController(DatabaseRepository db, ILogger<TestController> logger)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet("hello")]
    public ActionResult GetHello()
    {
        _logger.LogInformation("Hello world entered succesfully");
        return Ok(new { message = "Hello World" });
    }

    [Authorize]
    [HttpGet("database")]
    public ActionResult GetDatabase()
    {
        if (_db.TestConnection())
        {
            return Ok(new { message = "Database connection successful" });
        }
        else
        {
            _logger.LogError("Database connection failed");
            return StatusCode(500, new { message = "Database connection failed" });
        }
    }
}

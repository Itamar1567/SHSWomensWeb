
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new {message = "Hello World"});
    }
}

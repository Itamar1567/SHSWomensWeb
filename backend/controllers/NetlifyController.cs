using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/{controller}")]
public class NetlifyController : ControllerBase
{
    FrontendActions _frontendActions;
    public NetlifyController(FrontendActions frontendActions)
    {
        _frontendActions = frontendActions;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> RedeployNetlifyWebsite()
    {
        try
        {
            await _frontendActions.RedeployMainWeb();
            return Ok(new {message="Changes have been commited"});
        }
        catch(Exception ex)
        {
            Console.WriteLine("Unable to redeploy", ex);
            return StatusCode(500, new {message="Unable to redeploy website", error=ex});
        }
    }
}
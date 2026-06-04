using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/{controller}")]
public class NetlifyController : ControllerBase
{
    FrontendActions _frontendActions;
    public NetlifyController(FrontendActions frontendActions)
    {
        _frontendActions = frontendActions;
    }

    [EnableRateLimiting("authenticated_rate")]
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
            return StatusCode(500, new {message="Unable to redeploy website", error=ex});
        }
    }
}
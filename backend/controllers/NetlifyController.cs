using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/{controller}")]
public class NetlifyController : ControllerBase
{
    private readonly FrontendActions _frontendActions;
    private readonly ILogger<NetlifyController> _logger;

    public NetlifyController(FrontendActions frontendActions, ILogger<NetlifyController> logger)
    {
        _frontendActions = frontendActions;
        _logger = logger;
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
            _logger.LogError(ex, "Error occurred during Netlify website redeploy");
            return StatusCode(500, new {message="Unable to redeploy website"});
        }
    }
}
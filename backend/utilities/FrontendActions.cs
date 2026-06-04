
public class FrontendActions
{

    private readonly HttpClient _client;
    private readonly ILogger<FrontendActions> _logger;

    public FrontendActions(HttpClient client, ILogger<FrontendActions> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task RedeployMainWeb()
{
    var url = Environment.GetEnvironmentVariable("NetlifyBuildAPI");

    //If null the variable was not found
    if (string.IsNullOrEmpty(url))
    {
        _logger.LogError("NetlifyBuildAPI environment variable is not set");
        throw new Exception("NetlifyBuildAPI environment variable is not set");
    }

    try
    {
        var res = await _client.PostAsync(url, null);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while redeploying main web");
        throw new Exception("Error occurred while redeploying main web");
    }
}
}
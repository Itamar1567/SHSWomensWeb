
public class FrontendActions
{

    HttpClient _client;

    public FrontendActions(HttpClient client)
    {
        _client = client;
    }

    public async Task RedeployMainWeb()
{
    var url = Environment.GetEnvironmentVariable("NetlifyBuildAPI");

    //If null the variable was not found
    if (string.IsNullOrEmpty(url))
        throw new Exception("NetlifyBuildAPI environment variable is not set");

    try
    {
        var res = await _client.PostAsync(url, null);
        Console.WriteLine("Redeploy response: " + res.StatusCode);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error occurred while redeploying main web: " + ex);
        throw new Exception("Error occurred while redeploying main web");
    }
}
}
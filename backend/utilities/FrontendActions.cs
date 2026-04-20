
public class FrontendActions
{

    HttpClient _client;

    public FrontendActions(HttpClient client)
    {
        _client = client;
    }

    public async Task RedeployMainWeb()
    {
        try
        {
            var res = await _client.PostAsync(Environment.GetEnvironmentVariable("NetlifyBuildAPIKey"), null);
            Console.WriteLine("Redeploy response: " + res.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while redeploying main web: " + ex.Message);
            throw new Exception("Error occurred while redeploying main web");
        }
    }
}
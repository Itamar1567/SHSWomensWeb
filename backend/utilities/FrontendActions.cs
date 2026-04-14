
public class FrontendActions
{

    HttpClient _client;

    public FrontendActions(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> RedeployMainWeb()
    {
        try
        {
            var res = await _client.PostAsync(Environment.GetEnvironmentVariable("NetlifyBuildAPIKey"), null);
            Console.WriteLine("Redeploy response: " + res.StatusCode);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while redeploying main web: " + ex.Message);
            return false;
        }
    }
}
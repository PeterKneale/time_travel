namespace Demo.Lib.SystemTime;

public interface ISystemTimeTestHelper
{
    Task SetSystemTime(DateTime systemTime);
    Task ClearSystemTime();
}

public class SystemTimeTestHelper : ISystemTimeTestHelper
{
    private readonly HttpClient _client;

    public SystemTimeTestHelper(HttpClient client)
    {
        _client = client;
    }
    
    public async Task SetSystemTime(DateTime systemTime)
    {
        var url = $"{Constants.Endpoint}?SystemTime={systemTime:u}";
        var response = await _client.PostAsync(url, new StringContent(""));
        response.EnsureSuccessStatusCode();
    }

    public async Task ClearSystemTime()
    {
        var response = await _client.DeleteAsync(Constants.Endpoint);
        response.EnsureSuccessStatusCode();
    }
}
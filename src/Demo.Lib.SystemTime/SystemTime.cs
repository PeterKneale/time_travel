using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.Lib.SystemTime;

public interface ISystemTime
{
    Task<DateTime> GetSystemTime();
}

public class SystemTime : ISystemTime
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SystemTime> _logs;

    public SystemTime(HttpClient client, IConfiguration configuration, ILogger<SystemTime> logs)
    {
        _client = client;
        _configuration = configuration;
        _logs = logs;
    }

    public async Task<DateTime> GetSystemTime()
    {
        _logs.LogDebug("Check if the simulated system time feature is enabled");
        var flag = _configuration[Constants.Setting];
        if (flag == null || bool.Parse(flag) == false)
        {
            _logs.LogDebug("Simulation is not enabled, so return the current date time");
            return DateTime.UtcNow;
        }

        try
        {
            _logs.LogInformation("Simulation is enabled, so return the simulated date time");
            var value = await _client.GetStringAsync(Constants.Endpoint);
            _logs.LogInformation("Simulation time retrieved {SystemTime}", value);
            return DateTime.Parse(value).ToUniversalTime();
        }
        catch (Exception e)
        {
            _logs.LogError($"Error retrieving simulated time: {e.Message}", e);
            throw new SystemTimeUnavailableException(e);
        }
    }
}
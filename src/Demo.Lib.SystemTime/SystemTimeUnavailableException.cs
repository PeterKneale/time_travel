namespace Demo.Lib.SystemTime;

public class SystemTimeUnavailableException : Exception
{
    public SystemTimeUnavailableException(Exception ex): base("System time is unavailable",ex)
    {
        
    }
}
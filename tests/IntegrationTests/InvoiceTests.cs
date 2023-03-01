namespace IntegrationTests;

public class InvoiceTests : IClassFixture<ServiceFixture>
{ 
    private readonly HttpClient _client;
    private readonly ISystemTimeTestHelper _clock;

    public InvoiceTests(ServiceFixture service, ITestOutputHelper output)
    {
        service.OutputHelper = output;
        _client = service.CreateDefaultClient();
        _clock = new SystemTimeTestHelper(new HttpClient
        {
            BaseAddress = new Uri("https://localhost:8001")
        });
    }
    
    [Fact]
    public async Task Can_generate_invoice_at_specific_date()
    {
        // Arrange
        var now = new DateTime(2001, 01, 01, 9, 0, 0, DateTimeKind.Utc);
        await _clock.SetSystemTime(now);
        
        // Act
        var response = await _client.GetFromJsonAsync<Invoice>("/invoice");

        // Assert
        response.Amount.Should().Be(1);
        response.CreatedAt.Should().Be(now);
    }
}
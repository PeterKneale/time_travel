using Demo.Lib.SystemTime;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Service.Invoices.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly ISystemTime _time;

    public InvoiceController(ISystemTime time)
    {
        _time = time;
    }

    [HttpGet(Name = "GetInvoice")]
    public async Task<Invoice> Get()
    {
        var now = await _time.GetSystemTime();
        return new Invoice
        {
            CreatedAt = now,
            Amount = 1
        };
    }
}
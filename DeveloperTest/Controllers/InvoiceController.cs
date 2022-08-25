using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    [HttpGet("unpaid")]
    public ActionResult GetUnpaidInvoices()
    {
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult EditInvoice()
    {
        return NoContent();
    }

    [HttpPost("{id}")]
    public ActionResult PayInvoice()
    {
        return NoContent();
    }
}
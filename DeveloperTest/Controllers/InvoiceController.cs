using DeveloperTest.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator mediator;

    public InvoiceController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("unpaid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUnpaidInvoicesResponse))]
    public async Task<ActionResult> GetUnpaidInvoices()
    {
        var res = await mediator.Send(new GetUnpaidInvoicesRequest());
        return Ok(res);
    }

    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditInvoiceResponse))]
    public async Task<ActionResult> EditInvoice()
    {
        var res = await mediator.Send(new EditInvoiceRequest());
        return Ok(res);
    }

    [HttpPost("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayInvoiceResponse))]
    public async Task<ActionResult> PayInvoice([FromRoute] PayInvoiceRequest request)
    {
        var res = await mediator.Send(request);
        return Ok(res);
    }
}
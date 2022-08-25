using DeveloperTest.Models;
using DeveloperTest.Modules.Invoices.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Modules.Invoices;

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
    public async Task<ActionResult> GetUnpaidInvoices(CancellationToken ct) => await HandleRequest(new GetUnpaidInvoicesRequest(), ct);

    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditInvoiceResponse))]
    public async Task<ActionResult> EditInvoice(CancellationToken ct) => await HandleRequest(new EditInvoiceRequest(), ct);

    [HttpPost("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayInvoiceResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictResult))]
    public async Task<ActionResult> PayInvoice([FromRoute] PayInvoiceRequest request, CancellationToken ct) => await HandleRequest(request, ct);

    private async Task<ActionResult> HandleRequest<T>(IRequest<T> request, CancellationToken cancellationToken)
    {
        try
        {
            T res = await mediator.Send(request, cancellationToken);
            return Ok(res);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (AlreadyPaidException)
        {
            return Conflict();
        }
        catch (Exception)
        {
            return Problem("Internal server error");
        }
    }
}
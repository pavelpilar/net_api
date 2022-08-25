using DeveloperTest.Models;
using DeveloperTest.Modules.Invoices.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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

    [HttpPatch("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditInvoiceResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorResponse))]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(ApiErrorResponse))]

    public async Task<ActionResult> EditInvoice([FromBody] EditInvoiceRequest request, CancellationToken ct) => await HandleRequest(request, ct);

    [HttpPost("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PayInvoiceResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiErrorResponse))]
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
            return StatusCode(404, new ApiErrorResponse { Message = "Not Found" });
        }
        catch (AlreadyPaidException)
        {
            return StatusCode(404, new ApiErrorResponse { Message = "Invoice is already paid" });
        }
        catch (ReadonlyException e)
        {
            return StatusCode(405, new ApiErrorResponse { Message = "Invoice is already paid" });
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiErrorResponse { Message = "Internal server error" });
        }
    }
}
using DeveloperTest.Models;
using MediatR;

namespace DeveloperTest.Modules.Invoices.Handlers
{
    public class EditInvoiceHandler : IRequestHandler<EditInvoiceRequest, EditInvoiceResponse>
    {
        private readonly InvoiceContext ctx;

        public EditInvoiceHandler(InvoiceContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<EditInvoiceResponse> Handle(EditInvoiceRequest request, CancellationToken cancellationToken)
        {
            var invoice = ctx.Invoices.SingleOrDefault(x => x.Id == request.Id);

            if (invoice == null)
            {
                throw new NotFoundException();
            }

            if (invoice.Paid)
            {
                throw new ReadonlyException();
            }

            invoice.Description = request.Description ?? invoice.Description;
            invoice.Amount = request.Amount ?? invoice.Amount;

            ctx.SaveChanges();

            return new EditInvoiceResponse();
        }
    }

    public class EditInvoiceRequest : IRequest<EditInvoiceResponse>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
    }

    public class EditInvoiceResponse
    {
    }
}

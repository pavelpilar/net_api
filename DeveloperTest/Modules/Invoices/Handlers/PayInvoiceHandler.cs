using DeveloperTest.Models;
using MediatR;

namespace DeveloperTest.Modules.Invoices.Handlers
{
    public class PayInvoiceHandler : IRequestHandler<PayInvoiceRequest, PayInvoiceResponse>
    {
        private readonly InvoiceContext ctx;

        public PayInvoiceHandler(InvoiceContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<PayInvoiceResponse> Handle(PayInvoiceRequest request, CancellationToken cancellationToken)
        {
            var invoice = ctx.Invoices.SingleOrDefault(x => x.Id == request.Id);

            if(invoice == null)
            {
                throw new NotFoundException();
            }

            if(invoice.Paid)
            {
                throw new AlreadyPaidException();
            }

            invoice.Paid = true;
            await ctx.SaveChangesAsync(cancellationToken);

            return new PayInvoiceResponse();
        }
    }

    public class PayInvoiceRequest : IRequest<PayInvoiceResponse>
    {
        public int Id { get; set; }
    }

    public class PayInvoiceResponse
    {
    }
}

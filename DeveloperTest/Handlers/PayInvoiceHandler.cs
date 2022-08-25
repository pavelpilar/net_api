using MediatR;

namespace DeveloperTest.Handlers
{
    public class PayInvoiceHandler : IRequestHandler<PayInvoiceRequest, PayInvoiceResponse>
    {
        public async Task<PayInvoiceResponse> Handle(PayInvoiceRequest request, CancellationToken cancellationToken)
        {
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

using AutoMapper;
using DeveloperTest.Models;
using MediatR;

namespace DeveloperTest.Modules.Invoices.Handlers
{
    public class GetUnpaidInvoicesHandler : IRequestHandler<GetUnpaidInvoicesRequest, GetUnpaidInvoicesResponse>
    {
        private readonly InvoiceContext ctx;
        private readonly IMapper mapper;

        public GetUnpaidInvoicesHandler(InvoiceContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public async Task<GetUnpaidInvoicesResponse> Handle(GetUnpaidInvoicesRequest request, CancellationToken cancellationToken)
        {
            var data = ctx.Invoices.Where(x => !x.Paid);
            return new GetUnpaidInvoicesResponse() { Invoices = mapper.Map<ICollection<InvoiceDTO>>(data) };
        }
    }

    public class GetUnpaidInvoicesRequest : IRequest<GetUnpaidInvoicesResponse>
    {
    }

    public class GetUnpaidInvoicesResponse
    {
        public ICollection<InvoiceDTO> Invoices { get; set; }
    }
}

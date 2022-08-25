using MediatR;

namespace DeveloperTest.Handlers
{
    public class GetUnpaidInvoicesHandler : IRequestHandler<GetUnpaidInvoicesRequest, GetUnpaidInvoicesResponse>
    {
        public async Task<GetUnpaidInvoicesResponse> Handle(GetUnpaidInvoicesRequest request, CancellationToken cancellationToken)
        {
            return new GetUnpaidInvoicesResponse();
        }
    }

    public class GetUnpaidInvoicesRequest : IRequest<GetUnpaidInvoicesResponse>
    {
    }

    public class GetUnpaidInvoicesResponse
    {
    }
}

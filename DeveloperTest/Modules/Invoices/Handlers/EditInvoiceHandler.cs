﻿using MediatR;

namespace DeveloperTest.Modules.Invoices.Handlers
{
    public class EditInvoiceHandler : IRequestHandler<EditInvoiceRequest, EditInvoiceResponse>
    {
        public async Task<EditInvoiceResponse> Handle(EditInvoiceRequest request, CancellationToken cancellationToken)
        {
            return new EditInvoiceResponse();
        }
    }

    public class EditInvoiceRequest : IRequest<EditInvoiceResponse>
    {
    }

    public class EditInvoiceResponse
    {
    }
}
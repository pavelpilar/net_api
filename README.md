# Assignment description

We are creating a simple application to manage invoices. The .NET Solution is already prepared for you in this repository.

## Your task

Create a simple REST API. Access to API should be restricted by a secret key which is sent as a header value in the request.

Please prepare 3 endpoints which have following functionality:
* get collection of unpaid invoices
* pay an invoice (changing status to `paid`)
* edit an invoice (PATCH request)

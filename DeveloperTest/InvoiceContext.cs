using Microsoft.EntityFrameworkCore;

namespace DeveloperTest;

public class InvoiceContext : DbContext
{
    public InvoiceContext(DbContextOptions<InvoiceContext> options)
        : base(options)
    {
    }
}
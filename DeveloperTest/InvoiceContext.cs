using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;
namespace DeveloperTest;

public class InvoiceContext : DbContext
{
    public InvoiceContext(DbContextOptions<InvoiceContext> options)
        : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public string Description { get;set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
    }
}

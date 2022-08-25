using AutoMapper;
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

    public class InvoiceDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
    }

    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Paid, o => o.MapFrom(s => s.Paid))
                .ReverseMap();
        }
    }
}

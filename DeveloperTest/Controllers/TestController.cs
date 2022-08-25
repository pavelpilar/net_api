using Bogus;
using DeveloperTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Controllers
{
    #if DEBUG
    [ApiController]
    [Route("api/test")]

    public class TestController : ControllerBase
    {
        private readonly InvoiceContext ctx;

        public TestController(InvoiceContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost]
        public ActionResult GenerateTestData()
        {
            var invoices = new Faker<Invoice>()
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.Amount, f => f.Finance.Amount())
                .RuleFor(o => o.Paid, f => f.Random.Bool(0.6f))
                .GenerateBetween(10, 15);

            ctx.Invoices.AddRange(invoices);
            ctx.SaveChanges();

            return Ok();
        }
    }
    #endif
}

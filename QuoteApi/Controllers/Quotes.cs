using Microsoft.AspNetCore.Mvc;
using QuoteApi.Data;

namespace QuoteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Quotes : ControllerBase
{
    private readonly QuotesDataContext _context;
    public Quotes(QuotesDataContext context)
    {
        _context = context;
    }

    // GET
    [HttpGet]
    public string GetQuote()
    {

        var count = _context.Quotes.Count();

        var random = new Random();
        var rnd = random.Next(0,count);

        var quote = _context.Quotes.Find(rnd);
        if (quote != null)
        {
            return quote.Text;
        }
        return "error";
    }
}
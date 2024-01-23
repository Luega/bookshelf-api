using BookshelfApi.Dtos;
using BookshelfApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookshelfApi.Interfaces;

namespace QuoteshelfApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Quote>> GetQuotes()
        {
            return Ok(_quoteService.GetQuotes());
        }

        [HttpPost]
        public IActionResult PostQuote([FromBody] QuoteDto quoteDto)
        {
            if (quoteDto != null)
            {
                Quote quote = _quoteService.PostQuote(quoteDto);
                return CreatedAtAction(nameof(GetQuotes), new { id = quote.Id }, quote);
            }
            
            return BadRequest();
        }

    }
}
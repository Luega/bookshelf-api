using BookshelfApi.Dtos;
using BookshelfApi.Models;
using BookshelfApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace QuoteshelfApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private static readonly List<Quote> quotes = new()
        {
            new() { Id = Guid.NewGuid(), Text = "I am what I am", Author = "Luca Martinelli" },
            new() { Id = Guid.NewGuid(), Text = "I am what I am" },
            new() { Id = Guid.NewGuid(), Text = "I am what I am", Author = "Luca Martinelli" },
            new() { Id = Guid.NewGuid(), Text = "I am what I am", },
            new() { Id = Guid.NewGuid(), Text = "I am what I am", Author = "Luca Martinelli" },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Quote>> GetQuotes()
        {
            return quotes.Take(5).ToList();
        }

        [HttpPost]
        public IActionResult PostQuote([FromBody] QuoteDto quoteDto)
        {
            if (quoteDto != null)
            {
                Quote quote = QuoteMapper.FromDto(quoteDto);
                quotes.Insert(0, quote);
                return CreatedAtAction(nameof(GetQuotes), new { id = quote.Id }, quote);
            }
            
            return BadRequest();
        }

    }
}
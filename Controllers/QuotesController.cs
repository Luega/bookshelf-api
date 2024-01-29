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
        private readonly ILogger<QuotesController> _logger;

        public QuotesController(IQuoteService quoteService, ILogger<QuotesController> logger)
        {
            _quoteService = quoteService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Quote>> GetQuotes()
        {
            try
            {                
                var quotes = _quoteService.GetQuotes();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetQuotes.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostQuote([FromBody] QuoteDto quoteDto)
        {
            if (quoteDto == null)
                return BadRequest();

            try
            {
                Quote quote = _quoteService.PostQuote(quoteDto);
                return CreatedAtAction(nameof(GetQuotes), new { id = quote.Id }, quote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetQuotes.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }        
            
        }

    }
}
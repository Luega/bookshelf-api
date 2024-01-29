using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BookshelfApi.Models;
using BookshelfApi.Dtos;
using BookshelfApi.Interfaces;


namespace BookshelfApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {   
            try
            {
                var books = _bookService.GetBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetBooks.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            try
            {
                var book = _bookService.GetBook(id);
                if ( book != null)
                {
                    return Ok(book);
                }
                
                return NotFound();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred in GetBook.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest();

            try
            {   
                Book book = _bookService.PostBook(bookDto);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in PostBook.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult EditBook(Guid id, [FromBody] Book book)
        {
            if (id == Guid.Empty || book == null)
                return BadRequest();

            try 
            {
                if (_bookService.EditBook(id, book))
                {
                    return NoContent();
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in EditBook.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            try
            {
                if (_bookService.DeleteBook(id))
                {
                    return NoContent();
                }
                
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DeleteBook.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
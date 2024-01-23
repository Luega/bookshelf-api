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

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(Guid id)
        {
            var book = _bookService.GetBook(id);
            if ( book != null)
            {
                return Ok(book);
            }
            
            return NotFound();
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] BookDto bookDto)
        {
            if (bookDto != null)
            {   
                Book book = _bookService.PostBook(bookDto);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult EditBook(Guid id, [FromBody] Book book)
        {
            if (_bookService.EditBook(id, book))
            {
                return NoContent();
            }
            
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            if (_bookService.DeleteBook(id))
            {
                return NoContent();
            }
            
            return NotFound();
        }
    }
}
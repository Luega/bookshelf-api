using Microsoft.AspNetCore.Mvc;
using BookshelfApi.Models;
using BookshelfApi.Enums;


namespace BookshelfApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> books = new()
        {
            new() { Id = Guid.NewGuid(), Title = "The fellowship of the ring", Author = "J.R.R. Tolkien", Genre = BookGenre.Fantasy, PageCount = 423, Price = 23.50, PublishDate = new DateTime(1954, 05, 29) },
            new() { Id = Guid.NewGuid(), Title = "Project Hail Mary", Author = "Andy Weir", Genre = BookGenre.ScienceFiction, PageCount = 496, Price = 18.99, PublishDate = new DateTime(2021, 05, 4) },
            new() { Id = Guid.NewGuid(), Title = "I, Robot", Author = "Isaac Asimov", Genre = BookGenre.ScienceFiction, PageCount = 253, Price = 12.7, PublishDate = new DateTime(1950, 12, 2) },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book != null)
            {
                return book;
            }
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            if (book != null)
            {
                books.Add(book);
                return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
            }
            
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                existingBook.PageCount = book.PageCount;
                existingBook.Price = book.Price;
                existingBook.PublishDate = book.PublishDate;

                return NoContent();
            }
            
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book != null)
            {
                books.Remove(book);
                return NoContent();
            }
            
            return NotFound();
        }
    }
}
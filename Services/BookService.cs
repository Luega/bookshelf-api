using BookshelfApi.Dtos;
using BookshelfApi.Enums;
using BookshelfApi.Interfaces;
using BookshelfApi.Mappers;
using BookshelfApi.Models;

namespace BookshelfApi.Services
{
    public class BookService : IBookService
    {
        private static readonly List<Book> books = new()
        {
            new() { Id = Guid.NewGuid(), Title = "The fellowship of the ring", Author = "J.R.R. Tolkien", Genre = BookGenre.Fantasy, PageCount = 423, Price = 23.50, PublishDate = new DateTime(1954, 05, 29) },
            new() { Id = Guid.NewGuid(), Title = "Project Hail Mary", Author = "Andy Weir", Genre = BookGenre.ScienceFiction, PageCount = 496, Price = 18.99, PublishDate = new DateTime(2021, 05, 4) },
            new() { Id = Guid.NewGuid(), Title = "I, Robot", Author = "Isaac Asimov", Genre = BookGenre.ScienceFiction, PageCount = 253, Price = 12.7, PublishDate = new DateTime(1950, 12, 2) },
        };

        public IEnumerable<Book> GetBooks()
        {
            return books;
        }
        
        public Book? GetBook(Guid id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public Book PostBook(BookDto bookDto)
        {
            Book book = BookMapper.FromDto(bookDto);
            books.Add(book);

            return book;
        }

        public bool EditBook(Guid id, Book book)
        { 
            var existingBook = GetBook(id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                existingBook.PageCount = book.PageCount;
                existingBook.Price = book.Price;
                existingBook.PublishDate = book.PublishDate;
                
                return true;
            }

            return false;
        }

        public bool DeleteBook(Guid id)
        {
            var book = GetBook(id);
            if (book != null)
            {
                books.Remove(book);
                return true;
            }

            return false;
        }
    }
}
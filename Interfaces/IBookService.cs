using BookshelfApi.Dtos;
using BookshelfApi.Models;

namespace BookshelfApi.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        Book? GetBook(Guid id);
        Book PostBook(BookDto bookDto);
        bool EditBook(Guid id, Book book);
        bool DeleteBook(Guid id);
    }
}
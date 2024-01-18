using BookshelfApi.Dtos;
using BookshelfApi.Models;

namespace BookshelfApi.Mappers
{
    public class BookMapper
    {
        public static Book FromDto(BookDto bookDto)
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre,
                PageCount = bookDto.PageCount,
                Price = bookDto.Price,
                PublishDate = bookDto.PublishDate
            };
        }
    }
}
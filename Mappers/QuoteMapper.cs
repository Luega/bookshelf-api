using BookshelfApi.Dtos;
using BookshelfApi.Models;

namespace BookshelfApi.Mappers
{
    public class QuoteMapper
    {
        public static Quote FromDto(QuoteDto quoteDto)
        {
            return new Quote
            {
                Id = Guid.NewGuid(),
                Text = quoteDto.Text,
                Author = quoteDto.Author,
            };
        }
    }
}
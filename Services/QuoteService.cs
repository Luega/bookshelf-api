using BookshelfApi.Dtos;
using BookshelfApi.Interfaces;
using BookshelfApi.Mappers;
using BookshelfApi.Models;

namespace BookshelfApi.Services
{
    public class QuoteService : IQuoteService
    {
        private static readonly List<Quote> quotes = new()
        {
            new() { Id = Guid.NewGuid(), Text = "I am what I am", Author = "Luca Martinelli" },
            new() { Id = Guid.NewGuid(), Text = "I am what I am" },
            new() { Id = Guid.NewGuid(), Text = "I am what I am", Author = "Luca Martinelli" },
            new() { Id = Guid.NewGuid(), Text = "I am what I am", },
            new() { Id = Guid.NewGuid(), Text = "I am what I am", Author = "Luca Martinelli" },
        };

        public IEnumerable<Quote> GetQuotes()
        {
            return quotes.Take(5).ToList();
        }

        public Quote PostQuote(QuoteDto quoteDto)
        {
            Quote quote = QuoteMapper.FromDto(quoteDto);
            quotes.Insert(0, quote);
            
            return quote;
        }

    }
}
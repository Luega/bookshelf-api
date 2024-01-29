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
            new() { Id = Guid.NewGuid(), Text = "Be yourself; everyone else is already taken.", Author = "Oscar Wilde" },
            new() { Id = Guid.NewGuid(), Text = "Stay Hungry. Stay Foolish", Author = "Steve Jobs" },
            new() { Id = Guid.NewGuid(), Text = "The greatest glory in living lies not in never falling, but in rising every time we fall.", Author = "Nelson Mandela" },
            new() { Id = Guid.NewGuid(), Text = "You must be the change you wish to see in the world.",  Author = "Mahatma Gandhi" },
            new() { Id = Guid.NewGuid(), Text = "Just because something doesn’t do what you planned it to do doesn’t mean it’s useless.", Author = "Thomas Edison" },
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
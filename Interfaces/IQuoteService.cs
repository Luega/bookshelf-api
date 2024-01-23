using BookshelfApi.Dtos;
using BookshelfApi.Models;

namespace BookshelfApi.Interfaces
{
    public interface IQuoteService
    {
        IEnumerable<Quote> GetQuotes();
        Quote PostQuote(QuoteDto quoteDto);
    }
}
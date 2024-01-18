using BookshelfApi.Enums;

namespace BookshelfApi.Dtos
{
    public class BookDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public BookGenre Genre { get; set; }
        public int PageCount { get; set; }
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
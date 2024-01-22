namespace BookshelfApi.Models
{
    public class AuthToken
    {
        public string? Token { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
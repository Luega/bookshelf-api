using BookshelfApi.Models;

namespace BookshelfApi.Interfaces
{
    public interface ILoginService
    {
        bool IsAuthenticate(LoginCredentials loginCredentials);
        AuthToken CreateToken();
    }
}
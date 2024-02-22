using SharedP.Books;

namespace SharedP.Services.BookService.Token
{
    public interface IBookService
    {
        Task<ServiceResponse<List<Book>>> ReadBooksAsync();
        Task<ServiceResponse<string>> CreateBookAsync(Book book, string token);
        Task<ServiceResponse<string>> DeleteBookAsync(int id, string token);
        Task<ServiceResponse<string>> UpdateBookAsync(Book book, int id, string token);

    }
}

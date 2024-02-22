using SharedP.Books;

namespace SharedP.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponse<List<Book>>> ReadBooksAsync();
        Task<ServiceResponse<string>> CreateBookAsync(Book book);
        Task<ServiceResponse<string>> DeleteBookAsync(int id);
        Task<ServiceResponse<string>> UpdateBookAsync(Book book, int id);

    }
}

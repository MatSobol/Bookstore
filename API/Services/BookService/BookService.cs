using SharedP;
using SharedP.Services.BookService;
using SharedP.Books;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;
        public BookService(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<ServiceResponse<string>> CreateBookAsync(Book book)
        {
            try
            {
                if (_dataContext.Books.Any(x => x.Id == book.Id))
                {
                    return new ServiceResponse<string>()
                    {
                        Data = null,
                        Message = "Book with this id already exists",
                        Success = false,
                        CodeError = 400
                    };
                }
                _dataContext.Books.Add(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<string>()
                {
                    Data = "Book added",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<string>> DeleteBookAsync(int id)
        {
            try
            {
                var book = new Book() { Id = id };
                _dataContext.Books.Attach(book);
                _dataContext.Books.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<string>()
                {
                    Data = "Deleted Book",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<List<Book>>> ReadBooksAsync()
        {
            var books = await _dataContext.Books.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Book>>()
                {
                    Data = books,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Book>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false,
                    CodeError = 500
                };
            }

        }

        public async Task<ServiceResponse<string>> UpdateBookAsync(Book book, int id)
        {
            try
            {               
                var result = await DeleteBookAsync(id);
                if (result.Message != "Ok")
                {
                    return result;
                }
                result = await CreateBookAsync(book);
                if (result.Message != "Ok")
                {
                    return result;
                }
                var response = new ServiceResponse<string>()
                {
                    Data = "Updated info",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }
    }
}

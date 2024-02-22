using Microsoft.AspNetCore.Mvc;
using SharedP;
using SharedP.Services.BookService;
using SharedP.Books;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService BookService)
        {
            _bookService = BookService;
        }
            
        [HttpGet("ReadBooks")]
        public async Task<ActionResult<ServiceResponse<List<Book>>>> GetBooks()
        {
            var result = await _bookService.ReadBooksAsync();

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [Authorize]
        [HttpPost("createBook")] 
        public async Task<ActionResult<ServiceResponse<List<Book>>>> AddBook([FromBody] Book book)
        {
            var result = await _bookService.CreateBookAsync(book);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [Authorize]
        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Book>>>> DeleteBook([FromRoute] int id)
        {
            var result = await _bookService.DeleteBookAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
        [Authorize]
        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult<ServiceResponse<List<Book>>>> UpdateBook([FromBody] Book book, [FromRoute] int id)
        {
            var result = await _bookService.UpdateBookAsync(book, id);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }


    }
}

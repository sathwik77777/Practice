using AutoMapper;
using Practice_test1.Entities;
using Practice_test1.DTO;
using Practice_test1.Services;
using Practice_test1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using log4net;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice_test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private ILogger<BookController> logger;

        public BookController(IBookService bookService, IMapper mapper, IConfiguration configuration, ILogger<BookController> logger)
        {
            this.bookService = bookService;
            _mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpGet, Route("GetAllBooks")]
        //[Authorize]
        public IActionResult GetAllBooks()
        {
            try
            {
                List<Book> books = bookService.GetAllBooks();
                List<BookDTO> bookDTO = _mapper.Map<List<BookDTO>>(books);
                return StatusCode(200, bookDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetBookById/{bookId}")]
        //[Authorize] 
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                Book book = bookService.GetBookById(bookId);
                BookDTO bookDTO = _mapper.Map<BookDTO>(book);
                if (book != null)
                    return StatusCode(200, book);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("AddBook")]
        //[Authorize]
        public IActionResult Add(BookDTO bookDTO)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookDTO);
                bookService.AddBook(book);
                return StatusCode(200, book);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut, Route("EditBook")]
        //[Authorize]
        public IActionResult Edit(BookDTO bookDTO)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookDTO);
                bookService.UpdateBook(book);
                return StatusCode(200, book);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteBook/{bookId}")]
//[Authorize]
        public IActionResult Delete(int bookId)
        {
            try
            {
                bookService.DeleteBook(bookId);
                return StatusCode(200, new JsonResult($"Product with Id {bookId} is Deleted"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("SearchbyTitle")]
        //[Authorize]
        public IActionResult SearchByTitle(string title)
        {
            try
            {
                List<Book> book = bookService.GetBookByTitle(title);
                List<BookDTO> bookDTO = _mapper.Map<List<BookDTO>>(book);
                if (book != null)
                    return StatusCode(200, book);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("SearchbyAuthor")]
        //[Authorize]
        public IActionResult SearchByAuthor(string author)
        {
            try
            {
                List<Book> book = bookService.GetBookByAuthor(author);
                List<BookDTO> bookDTO = _mapper.Map<List<BookDTO>>(book);
                if (book != null)
                    return StatusCode(200, book);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("SearchbyGenre")]
        //[Authorize]
        public IActionResult SearchByGenre(string genre)
        {
            try
            {
                List<Book> book = bookService.GetBookByGenre(genre);
                List<BookDTO> bookDTO = _mapper.Map<List<BookDTO>>(book);
                if (book != null)
                    return StatusCode(200, book);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

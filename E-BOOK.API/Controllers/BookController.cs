using E_BOOK.API.Repository.Repository_Interface;
using E_BOOK.API.Service.Service_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.API.Controllers
{
    [Route("api/book")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BookController : ControllerBase
    {
        private readonly IE_BookRepository _BookRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<BookController> _logger;

        public BookController(IE_BookRepository e_BookRepository, ICloudinaryService cloudinaryService, ILogger<BookController> logger)
        {
            _BookRepository = e_BookRepository;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }
        //IZUCHUKWU
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAllBooks(int pageNumber, int perPageSize)
        {
            try
            {
                var books = await _BookRepository.GetALLBook();
                if (books != null && books.Count() > 0)
                {
                    var paged = _BookRepository.PaginatedAsync(books.ToList(), pageNumber, perPageSize);
                    return Ok(paged);
                }
                return NotFound("There is no contact");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing get all paginated book request");
                return BadRequest("Error retrieving book from database");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid parameter");
                }

                var result = await _BookRepository.GetBookById(id);

                if (result == null)
                {
                    return NotFound("No record found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while get book by id request");
                return BadRequest("Error getting book from database");
            }
        }
        //IZUCHUKWU
        [Authorize(Roles = "ADMIN")]
        [HttpGet("search/title")]
        public async Task<IEnumerable<SearchBookDTO>> SearchTitle_IZU(string title)
        {
            var result = await _BookRepository.SearchTitle_IZU(title);

            if (result.Any())
            {
                return result;
            }
            return null;
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("search/author")]
        public async Task<IEnumerable<SearchBookDTO>> SearchAuthor_IZU(string author)
        {
            var result = await _BookRepository.SearchAuthor_IZU(author);

            if (result.Any())
            {
                return result;
            }
            return null;
        }
        [AllowAnonymous]
        [HttpGet("popular")]
        public async Task<ActionResult> GetAllPopularBooks(int pageNumber, int perPageSize)
        {
            try
            {
                var PopularBooks = _BookRepository.PopularBook();
                if (PopularBooks != null && PopularBooks.Count() > 0)
                {
                    var paged = _BookRepository.PaginatedAsync(PopularBooks.ToList(), pageNumber, perPageSize);
                    return Ok(paged);
                }
                return Ok(new
                {
                    Message = "There is no pupolar book",
                    Books = new List<Book>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing get all popular book request");
                return BadRequest("Error retrieving Popularbooks from database");
            }
        }
        [AllowAnonymous]
        [HttpGet("recent")]
        public async Task<ActionResult> RecentBooks(int pageNumber, int perPageSize)
        {
            try
            {
                var RecentBooks = _BookRepository.RecentBook();
                if (RecentBooks != null && RecentBooks.Count() > 0)
                {
                    var paged = _BookRepository.PaginatedAsync(RecentBooks.ToList(), pageNumber, perPageSize);
                    return Ok(paged);
                }
                return Ok(new
                {
                    Message = "There is no pupolar book",
                    Books = new List<Book>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing get all recent book request");
                return BadRequest("Error retrieving Recentbooks from database");
            }
        }
        //IZUCHUKWU
        [Authorize(Roles = "ADMIN")]
        [HttpPost("add")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var addbook = await _BookRepository.CreateBook_IZU(createBookDTO);
                if (addbook == true)
                {
                    return Ok(new
                    {
                        Message = "Book was added successfully"
                    });
                }
                return StatusCode(StatusCodes.Status501NotImplemented, "Not Book not Added to Database");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while processing create book request");
                return StatusCode(StatusCodes.Status501NotImplemented, "Not Book not Added to Database");
            }


        }
        [Authorize(Roles = "ADMIN")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            try
            {
                var updatedBook = await _BookRepository.UpdateBook_IZU(id, updateBookDTO);
                var response = new UpdateDTOResponse()
                {
                    Id = updatedBook.Id,
                    Title = updatedBook.Title,
                    Author = updatedBook.Author,
                    NoPage = updatedBook.NoPage,
                    Description = updatedBook.Description,
                    ISBN = updatedBook.ISBN,
                    AddedToLib = updatedBook.AddedToLib,
                    Publisher = updatedBook.Publisher,
                    PublisherDate = updatedBook.PublisherDate,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing update book by id request");
                return StatusCode(StatusCodes.Status501NotImplemented, "Not Book not updated to Database");
            }

        }
        [Authorize(Roles = "ADMIN")]
        [HttpPatch("photos/{id}")]
        public async Task<IActionResult> UploadPhoto(IFormFile file, int id)
        {
            try
            {
                var book = await _BookRepository.GetBookById(id);
                if (book == null)
                {
                    return NotFound("Book not available");
                }

                var result = await _cloudinaryService.UploadPhoto(file, id);

                if (result != null)
                {
                    book.BookImg = result.Url.ToString();
                    await _BookRepository.UpdateBookImg(book);
                    return Ok(result.Url.ToString());
                }
                else
                {
                    return BadRequest("Upload was not successful");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing book image upload by id request");
                return StatusCode(StatusCodes.Status501NotImplemented, "Book Image not Added to Database");
            }

        }
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {

                var result = await _BookRepository.RemoveBook(id);


                if (!result)
                {
                    return NotFound("Failed to delete Book");
                }

                return Ok($"Book Deleted {result}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing delete book by id request");
                return BadRequest("Error deleting book from database");
            }


        }

    }
}

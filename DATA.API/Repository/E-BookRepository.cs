using E_BOOK.API.Repository.Repository_Interface;
using Microsoft.EntityFrameworkCore;
using MODEL;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.API.Repository
{
    public class E_BookRepository : IE_BookRepository
    {
        private readonly E_BookDbContext _BookDbContext;

        public E_BookRepository(E_BookDbContext e_BookDbContext)
        {
            _BookDbContext = e_BookDbContext;
        }


        public async Task<IEnumerable<Book>> GetALLBook()
        {
            var result = await _BookDbContext.Books.ToListAsync();
            return result;
        }
        public async Task IncrementView(int Id)
        {
            var oldresult = await _BookDbContext.Books.FirstOrDefaultAsync(x => x.Id == Id);
            oldresult.ViewBook += 1;
            _BookDbContext.Update(oldresult);
            await _BookDbContext.SaveChangesAsync();
        }
        public async Task<Book> GetBookById(int Id)
        {

            await IncrementView(Id);
            var result = await _BookDbContext.Books.Include(x => x.Reviews).FirstOrDefaultAsync(b => b.Id == Id);
            if(result == null)
            {
                throw new Exception("book not available");
            }
            //var averageRating = result.Reviews.Sum(x => x.Rating) / result.Reviews.Count();
            
            //var bookDTO = new BookDTO()
            //{
            //  ISBN =  result.ISBN,
            //  NoPage = result.NoPage,
            //  AddedToLib = result.AddedToLib,
            //  Author = result.Author,
            //  ViewBook = result.ViewBook,
            //  Description = result.Description,
            //  Id = result.Id,
            //  BookImg = result.BookImg,
            //  Title = result.Title,
            //  PublisherDate = result.PublisherDate,
            //  Publisher = result.Publisher,
            //  Reviews = result.Reviews,
            //  rating = averageRating,
            //  AppUserId = result.AppUserId,
            //};

            return result;
        }

        public async Task<bool> RemoveBook(int Id)
        {
            var result = await _BookDbContext.Books.FirstOrDefaultAsync(x => x.Id == Id);
            if (result != null)

                _BookDbContext.Books.Remove(result);
            var status = _BookDbContext.SaveChangesAsync();

            if (status.IsCompleted)
                return true;
            return false;
        }


        public async Task<Book> UpdateBookImg(Book book)
        {
            var BookToUpdate = await GetBookById(book.Id);
            if (BookToUpdate != null)
            {
                await _BookDbContext.SaveChangesAsync();
                return BookToUpdate;
            }
            return null;
        }

        public PaginatedBooks PaginatedAsync(List<Book> books, int pageNumber, int perPageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var totalCount = books.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var paginated = books.Skip((pageNumber - 1) * perPageSize).Take(perPageSize).ToList();
            var result = new PaginatedBooks
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                Books = paginated
            };
            return result;
        }

        public IEnumerable<Book> PopularBook()
        {
            var Views = _BookDbContext.Books.OrderByDescending(b => b.ViewBook).ToList();

            return Views;

        }
        public IEnumerable<Book> RecentBook()
        {
            DateTime OneWeekAgo = DateTime.Now.AddDays(-10);
            var Views = _BookDbContext.Books.Where(b => b.AddedToLib >= OneWeekAgo).OrderByDescending(b => b.AddedToLib).ToList();

            return Views;

        }

        //IZUCHUKWU

        public async Task<bool> CreateBook_IZU(CreateBookDTO createBookDTO)
        {
            var _book = new Book()
            {
                AppUserId = createBookDTO.AppUserId,
                Title = createBookDTO.Title,
                Author = createBookDTO.Author,
                NoPage = createBookDTO.NoPage,
                Description = createBookDTO.Description,
                ISBN = createBookDTO.ISBN,
                AddedToLib = createBookDTO.AddedToLib,
                Publisher = createBookDTO.Publisher,
                PublisherDate = createBookDTO.PublisherDate,
                ViewBook = createBookDTO.ViewBook,
                BookImg = string.Empty
            };
            var addbook = await _BookDbContext.Books.AddAsync(_book);
            var result = await _BookDbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<Book> UpdateBook_IZU(int bookId, UpdateBookDTO updateBookDTO)
        {
            var book = _BookDbContext.Books.FirstOrDefault(n => n.Id == bookId);
            if (book != null)
            {

                book.Title = updateBookDTO.Title;
                book.Author = updateBookDTO.Author;
                book.NoPage = updateBookDTO.NoPage;
                book.Description = updateBookDTO.Description;
                book.ISBN = updateBookDTO.ISBN;
                book.AddedToLib = updateBookDTO.AddedToLib;
                book.Publisher = updateBookDTO.Publisher;
                book.PublisherDate = updateBookDTO.PublisherDate;
                await _BookDbContext.SaveChangesAsync();
            }
            return book;
        }

        public async Task<IEnumerable<SearchBookDTO>> SearchAuthor_IZU(string author)
        {
            var search = await _BookDbContext.Books
                .Where(book => book.Author.ToLower().Contains(author.ToLower()))
                .Select(book => new SearchBookDTO
                {
                    Title = book.Title,
                    Author = book.Author,
                    NoPage = book.NoPage,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    AddedToLib = book.AddedToLib,
                    Publisher = book.Publisher,
                    PublisherDate = book.PublisherDate
                })
                .ToListAsync();
            return search;
        }

        public async Task<IEnumerable<SearchBookDTO>> SearchTitle_IZU(string title)
        {
            var search = await _BookDbContext.Books
                .Where(book => book.Title.ToLower().Contains(title.ToLower()))
                .Select(book => new SearchBookDTO
                {
                    Title = book.Title,
                    Author = book.Author,
                    NoPage = book.NoPage,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    AddedToLib = book.AddedToLib,
                    Publisher = book.Publisher,
                    PublisherDate = book.PublisherDate
                })
                .ToListAsync();

            return search;
        }
    }
}

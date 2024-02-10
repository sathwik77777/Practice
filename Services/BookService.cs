using Practice_test1.Entities;
using Practice_test1.Database;

namespace Practice_test1.Services
{
    public class BookService : IBookService
    {
        private readonly Mycontext Context;
        public BookService(Mycontext context)
        {
            Context = context;
        }
        public void AddBook(Book book)
        {
            try
            {
                Context.Books.Add(book);
                Context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteBook(int bookId)
        {
            try
            {

                Book book = Context.Books.SingleOrDefault(b => b.BookId == bookId);
                Context.Books.Remove(book);
                Context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Book> GetAllBooks()
        {
            try
            {
                return Context.Books.ToList(); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Book> GetBookByAuthor(string author)
        {
            try
            {

                return Context.Books.Where(b => b.Author == author).ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Book> GetBookByGenre(string genre)
        {
            try
            {

                return Context.Books.Where(b => b.Genre == genre).ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Book GetBookById(int bookId)
        {
            try
            {

                Book book = Context.Books.SingleOrDefault(p => p.BookId == bookId);
                return book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Book> GetBookByTitle(string title)
        {
            try
            {

                return Context.Books.Where(b => b.Title == title).ToList();

            }
            catch (Exception)
            {

                throw;
            };
        }

        public void UpdateBook(Book book)
        {
            try
            {
                Context.Books.Update(book);
                Context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Practice_test1.Entities;

namespace Practice_test1.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks(); 
        
        Book GetBookById(int bookId); 
        List<Book> GetBookByTitle(string title);
        List<Book> GetBookByAuthor(string author);
        List<Book> GetBookByGenre(string genre);
        void AddBook(Book book); 
        void UpdateBook(Book book);
        void DeleteBook(int bookId); 
    }
}

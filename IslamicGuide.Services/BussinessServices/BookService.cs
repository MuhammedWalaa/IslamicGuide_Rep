using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Books;
using System.Collections.Generic;
using System.Linq;

namespace IslamicGuide.Services.Services
{
    public class BookService
    {
        private readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        public BookService()
        {
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
        }
        public List<BookGridVM> GetAllBooks()
        {
            List <BookGridVM> booksGrid = new List<BookGridVM>();
            var books = _DbContext.Books.ToList();
            foreach (var item in books)
            {
                booksGrid.Add(new BookGridVM() { ID=item.ID,Title = item?.Title, Version = item?.Version });
            }
            //books
            return booksGrid;
        }
        public int CountBooks()
        {
            return _DbContext.Books.Count();
        }
    }
}

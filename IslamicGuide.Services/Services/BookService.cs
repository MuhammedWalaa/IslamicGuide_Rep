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
            List<Book> x = _DbContext.Books.ToList();
            return new List<BookGridVM>();
        }
    }
}

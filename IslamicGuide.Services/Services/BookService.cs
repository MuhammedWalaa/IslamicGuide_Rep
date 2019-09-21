using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Books;
using System.Collections.Generic;

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
            
            return new List<BookGridVM>();
        }
    }
}

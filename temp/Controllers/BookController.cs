using IslamicGuide.Data.ViewModels.Books;
using IslamicGuide.Services.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{
    public class BookController : BaseController
    {

        // GET: Book
        private readonly BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }
        public ActionResult Index()
        {

            List<BookGridVM> c = _bookService.GetAllBooks();
            return View();
        }

    }
}

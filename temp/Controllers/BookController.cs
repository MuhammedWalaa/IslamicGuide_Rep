using IslamicGuide.Services.Services;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{
    public class BookController : BaseController
    {
        private readonly BookService _bookService;
        public BookController()
        {
            _bookService = new BookService();
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

    }
}

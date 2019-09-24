﻿using IslamicGuide.Data.ViewModels.Books;
using IslamicGuide.Services.Services;
using System.Collections.Generic;
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

            List<BookGridVM> c = _bookService.GetAllBooks();
            return View();
        }

    }
}

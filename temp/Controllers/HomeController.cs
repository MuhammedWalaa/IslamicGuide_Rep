using IslamicGuide.Data.ViewModels.Home;
using IslamicGuide.Services.BussinessServices;
using IslamicGuide.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{

    public class HomeController : BaseController 
    {
        private readonly SubjectService _subjectService;
        private readonly BookService _bookService;
        public HomeController()
        {
            _subjectService = new SubjectService();
            _bookService = new BookService();
        }
        public ActionResult Index()
        {
            HomeVM hm = new HomeVM();
            hm.Subject = _subjectService.GetMainSubjects();
            hm.BookCount = _bookService.CountBooks();
            hm.SubjectCount = _subjectService.CountSubjects();
            return View(hm);
        }
        public ActionResult SetLanguage(string lang)
        {
            var Path = Request.UrlReferrer.AbsolutePath;
            HttpCookie cultureCookie = new HttpCookie("culture");
            cultureCookie.Value = lang;
            cultureCookie.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(cultureCookie);
            LanguageSetting.SetLanguage(lang);
            return Redirect(Path);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
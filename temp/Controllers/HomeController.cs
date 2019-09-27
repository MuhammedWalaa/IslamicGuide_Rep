using IslamicGuide.Data.ViewModels.Home;
using IslamicGuide.Services.BussinessServices;
using IslamicGuide.Services.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IslamicGuide.Services.Utilities;

namespace IslamicGuide.App.Controllers
{

    public class HomeController : BaseController
    {
        private readonly CommonServices _commonServices;
        private readonly SubjectService _subjectService;
        private readonly BookService _bookService;
        private readonly RouteService _routeService;
        public HomeController()
        {
            _subjectService = new SubjectService();
            _bookService = new BookService();
            _routeService=new RouteService();
            _commonServices = new CommonServices();
            
        }
        public ActionResult Index()
        {
            _routeService.RouteHandling("Home","Home","Index",null,Routes);
            HomeVM hm = new HomeVM
            {
                Subject = _subjectService.GetMainSubjects(),
                BookCount = _bookService.CountBooks(),
                SubjectCount = _subjectService.CountSubjects()
            };
            return View(hm);
        }
        public ActionResult SetLanguage(string lang)
        {
            string Path = Request.UrlReferrer.AbsolutePath;
            HttpCookie cultureCookie = new HttpCookie("culture")
            {
                Value = lang,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.SetCookie(cultureCookie);
            LanguageSetting.SetLanguage(lang);
            return Redirect(Path);

        }

        public void AddSubscriber(string email)
        {
            _commonServices.AddSubscriber(email);
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

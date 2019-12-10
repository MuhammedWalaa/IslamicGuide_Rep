using IslamicGuide.Data.ViewModels.Home;
using IslamicGuide.Services.BussinessServices;
using IslamicGuide.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IslamicGuide.Data.ViewModels.Shared;
using IslamicGuide.Services.Utilities;
using Resources;

namespace IslamicGuide.App.Controllers
{

    public class HomeController : BaseController
    {
        private readonly CommonServices _commonServices;
        private readonly SubjectService _subjectService;
        private readonly BookService _bookService;
        private readonly RouteService _routeService;
        private readonly StaticDataService _staticDataService;
        public HomeController()
        {
            _subjectService = new SubjectService();
            _bookService = new BookService();
            _routeService=new RouteService();
            _commonServices = new CommonServices();
            _staticDataService = new StaticDataService();
        }
        [HttpGet]
        public ActionResult Index()
        {
            Routes=new List<Route>();
            _routeService.RouteHandling(Request.Url.OriginalString, null,"Home","الرئيسية","Home","Index",null,Routes);
            HomeVM hm = new HomeVM
            { 
                FirstBanner = _staticDataService.GetFirstBannerData(LangCode),
                SecBanner = _staticDataService.GetSecBannerData(LangCode),
                Subject = _subjectService.GetMainSubjects(LangCode),
                BookCount = _bookService.CountBooks(),
                SubjectCount = _subjectService.CountSubjects(),
                ThirdBanner = _staticDataService.GetThirdBannerData(LangCode)
            };
            return View(hm);
        }
        [HttpPost]
        public ActionResult Index(string author, string email, string comment)
        {
            var res = _staticDataService.SaveContactUs(author, email, comment);
            if (res)
            {
                _routeService.RouteHandling(Request.Url.OriginalString, null, "Home", "الرئيسية", "Home", "Index", null, Routes);
                HomeVM hm = new HomeVM
                {
                    Subject = _subjectService.GetMainSubjects(LangCode),
                    BookCount = _bookService.CountBooks(),
                    SubjectCount = _subjectService.CountSubjects()
                };
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        public ActionResult SetLanguage(string lang)
        {
            var url = HttpContext.Request.UrlReferrer.ToString();
            HttpCookie cultureCookie = new HttpCookie("culture")
            {
                Value = lang,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.SetCookie(cultureCookie);
            LanguageSetting.SetLanguage(lang);
            return Redirect(url);

        }

        public ActionResult AddSubscriber(string email)
        {
            try
            {
                _commonServices.AddSubscriber(email);
                TempData["Added"] = true;

            }
            catch(Exception ex)
            {
                TempData["Wrong"] = true;
            }

            TempData.Keep();
            return Redirect(Request.UrlReferrer.OriginalString);
        }
        public ActionResult About()
        {
            ViewBag.Message = _staticDataService.GetAboutPageDataSection(LangCode);
            ViewBag.BookCount = _bookService.CountBooks();
            ViewBag.SubjectCount = _subjectService.CountSubjects();
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.False = false;
            ViewBag.True = false;
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string author, string email, string comment, string phone)
        {
            ViewBag.Message = "Your contact page.";
            var res = _staticDataService.SaveContactUs(author, email, comment, phone);
            if (!res || phone.Contains("e"))
            {

                ViewBag.False = true;
                ViewBag.Failed = LayoutResource.ContactFailed;
                return View("Error");
            }
            
            ViewBag.True = true;
            ViewBag.Success = LayoutResource.ContactSuccess;
            return View();
            

        }
        public ActionResult Error()
        {
            return View();
        }
    }
}

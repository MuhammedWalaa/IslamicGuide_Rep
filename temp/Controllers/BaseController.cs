using IslamicGuide.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using IslamicGuide.Data.ViewModels.Shared;

namespace IslamicGuide.App.Controllers
{
    public class BaseController : Controller
    {

        public Guid? SystemUserId => Request.Cookies["UserId"] == null ? (Guid?)null : new Guid(Request.Cookies["UserId"].Value);
        public string LangCode => Request.Cookies["culture"] == null ? string.Empty : Request.Cookies["culture"].Value;
        public static List<Route> Routes;
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (Routes == null)
            {
                Routes= new List<Route>();
            }
            string lang;
            System.Web.HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                string[] userLanguage = Request.UserLanguages;
                lang = userLanguage != null ? userLanguage[0] : "ar";
            }
            LanguageSetting.SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            StaticDataService staticData = new StaticDataService();
            ViewBag.PraysTime = staticData.GetPraysTime(LangCode);
            ViewBag.StaticData = staticData.GetLayoutStaticData(LangCode);
            ViewBag.Routes = Routes;
        }

    }
}

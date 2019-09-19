using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace temp
{
    public static class LanguageSetting
    {
        public static string GetCurrentLanguage()
        {
            var langName = "en";
            var langcookie = HttpContext.Current.Request.Cookies.Get("culture");
            if (!string.IsNullOrEmpty(langcookie?.Value))
                langName = langcookie.Value;
            return langName;
        }
        public static void SetLanguage(string lang)
        {
            try
            {
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                // Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
            catch (Exception) { }
        }
    }

}
using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace IslamicGuide.App
{
    public static class LanguageSetting
    {
        public static string GetCurrentLanguage()
        {
            string langName = "en";
            HttpCookie langcookie = HttpContext.Current.Request.Cookies.Get("culture");
            if (!string.IsNullOrEmpty(langcookie?.Value))
            {
                langName = langcookie.Value;
            }

            return langName;
        }
        public static void SetLanguage(string lang)
        {
            try
            {
                CultureInfo cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                // Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
            catch (Exception) { }
        }
    }

}

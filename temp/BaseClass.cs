namespace IslamicGuide.App
{
    public class BaseClass : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string lang = Request.QueryString["lang"];
            if (!string.IsNullOrEmpty(lang))
            {
                Culture = lang;
                UICulture = lang;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace temp
{
    public class BaseClass : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            var lang = Request.QueryString["lang"];
            if (!string.IsNullOrEmpty(lang))
            {
                Culture = lang;
                UICulture = lang;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Shared
{
    public class Route
    {
       
        public string Controller { get; set; }
        public Title Text { get; set; }
        public string ActionName { get; set; }
        public int? Id { get; set; }
        public string Uri { get; set; }

    }

    public class Title
    {
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
    }
}

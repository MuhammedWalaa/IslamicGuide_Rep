using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Shared
{
    public class PageFilterModel
    {
        public string LangCode { get; set; }
        public int Skip { get; set; }
        public int  PageSize { get; set; }
    }
}

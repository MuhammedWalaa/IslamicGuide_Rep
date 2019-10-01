using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Shared
{
    public class PageListResult<T> where T : class
    {
        public int RowsCount { get; set; }
        public List<T>  DataList { get; set; }
    }
}

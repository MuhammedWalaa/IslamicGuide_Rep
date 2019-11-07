using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Shared
{
    public class KeyValueList
    {
        public List<string> Words { get; set; }
        public int FirstAya { get; set; }
        public int LastAya { get; set; }
    }
}

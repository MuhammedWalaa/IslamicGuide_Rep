using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Subjects
{
    public class SubjectVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string  Title_English { get; set; }
        public string ParentTitle { get; set; }
    }
}

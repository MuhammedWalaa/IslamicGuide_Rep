using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Shared;

namespace IslamicGuide.Data.ViewModels.Subjects
{
    public class SubSubjectPageVM
    {


        public List<SubjectVM> subjectVM { get; set; }

        public List<SubjectVM> subjectsDropdown { get; set; }
        public int Id { get; set; }
        public Boolean HasPosition { get; set; }

        public PageListResult<SubjectVM> DataList { get; set; }


    }
}

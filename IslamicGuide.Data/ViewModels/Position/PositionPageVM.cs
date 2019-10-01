using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Shared;

namespace IslamicGuide.Data.ViewModels.Position
{
    public class PositionPageVM
    {
        public int SubjectId { get; set; }
        public PageListResult<PositionVM> DataList { get; set; }


    }
}

using IslamicGuide.Data.ViewModels.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Home
{
    public class HomeVM
    {
        public BannerVM FirstBanner { get; set; }
        public BannerVM SecBanner { get; set; }
        public BannerVM ThirdBanner { get; set; }
        public List<SubjectVM> Subject { get; set; }
        public int BookCount { get; set; }
        public int SubjectCount { get; set; }
    }
}

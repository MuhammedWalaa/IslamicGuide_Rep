using System.Collections.Generic;

namespace IslamicGuide.Data.ViewModels.Shared
{
    public class LayoutStaticDataVM
    {
        public List<PraysVM> PraysVm { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string There { get; set; }
    }

    public class PraysVM
    {
        public string Name { get; set; }
        public string Time { get; set; }
    }
}

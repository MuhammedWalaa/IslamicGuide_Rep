using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Position
{
    public class PositionVM
    {
        public int PosID { get; set; }
        public int SuraNum { get; set; }
        public string SuraTitle { get; set; }
        public int AyatCount { get; set; }
        public string QuranWords { get; set; }
    }
}

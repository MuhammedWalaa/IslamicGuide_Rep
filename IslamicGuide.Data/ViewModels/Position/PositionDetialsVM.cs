﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Position
{
    public class PositionDetialsVM
    {
        public string SuraTitle { get; set; }
        public string SuraTitle_English { get; set; }
        public string PositionQuranWords { get; set; }
        public string NextAyaWords { get; set; }
        public string PrevAyaWords { get; set; }
        public List<BookContentVM> BookContent { get; set; }
        public string Title { get; set; }
    }
}

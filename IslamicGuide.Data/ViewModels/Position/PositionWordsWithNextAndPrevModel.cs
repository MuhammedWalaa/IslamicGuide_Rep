using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Data.ViewModels.Position
{
    public class PositionWordsWithNextAndPrevModel
    {
        public List<string> Words { get; set; }
        public string PreviousAyaWords { get; set; }
        public string NextAyaWords { get; set; }
    }
}

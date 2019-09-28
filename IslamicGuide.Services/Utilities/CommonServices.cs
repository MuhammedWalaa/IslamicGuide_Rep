using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Services.Utilities
{
    public class CommonServices
    {
        private readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        public CommonServices()
        {
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
        }
        public List<string> GetQuranWords(int from, int to, int ayatCount)
        {
            List<string> lastWords = new List<string>();
            List<int> ayatDistinct = new List<int>();
            Dictionary<int, string> word_Aya = new Dictionary<int, string>();
            var allWords = _DbContext.QuranWords.OrderBy(e => e.p_Id).Skip(from - 1).Take(to + 1 - from).Select(x => new { word = x.Word, aya = x.AyaNum }).ToList();
            var finalWords = allWords.Select(x => x.word).ToList();
            var ayatRange = allWords.Select(e => e.aya).Distinct().ToList();
            foreach (var item in ayatRange)
            {
                if (item != ayatRange[ayatRange.Count() - 1])
                {
                    //var indexToInsert = allWords.Where(e => e.aya == item).Select(x => new { x.word, x.aya});
                    allWords.Insert(allWords.FindLastIndex(s => s.aya == item) + 1, new { word = "", aya = item });
                    var inde = allWords.FindLastIndex(s => s.aya == item);
                    finalWords.Insert(allWords.FindLastIndex(s => s.aya == item), "(" + item + ")");
                }
            }

            return finalWords;
        }

    }
}

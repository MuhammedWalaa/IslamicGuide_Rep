using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Shared;
using IslamicGuide.Data.ViewModels.Subjects;
using IslamicGuide.Services.BussinessServices;
using IslamicGuide.Data.ViewModels.Position;

namespace IslamicGuide.Services.Utilities
{
    public class CommonServices
    {
        private readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        private readonly SubjectService _subjectService;
        public CommonServices()
        {
            _subjectService = new SubjectService();
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
        }
        public List<string> GetQuranWords(int from, int to, int ayatCount,string langCode)
        {
            var allWords = _DbContext.QuranWords.OrderBy(e => e.ID).Skip(from - 1).Take(to + 1 - from).Select(x => new { word = langCode=="en"&&x.Word_English!=null? x.Word_English:x.Word, aya = x.AyaNum }).ToList();
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

        public PositionWordsWithNextAndPrevModel GetQuranWordsWithNextPrevAyah(int from, int to, int ayatCount, string langCode)
        {
            PositionWordsWithNextAndPrevModel resultModel = new PositionWordsWithNextAndPrevModel(); 
            string previousAya="";
            string nextAya="";
            bool nomoreAyat = false;
             var allWords = _DbContext.QuranWords.OrderBy(e => e.ID).Skip(from - 1).Take(to + 1 - from).Select(x => new { word = langCode == "en" && x.Word_English != null ? x.Word_English : x.Word, aya = x.AyaNum }).ToList();
            var finalWords = allWords.Select(x => x.word).ToList();
            var ayatRange = allWords.Select(e => e.aya).Distinct().ToList();
            var lastAya = ayatRange[ayatRange.Count-1];
            var firstAya = ayatRange[0];
            if (firstAya == lastAya)
                nomoreAyat = true;
            int firstAyaSoraId = _DbContext.QuranWords.FirstOrDefault(x => x.ID == from).SoraID.Value;
            if (firstAya != 1)
            {
                if(langCode=="en")
                    previousAya = _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (firstAya-1)).Aya_English ==null
                        ? _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (firstAya - 1)).Aya 
                        : _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (firstAya - 1)).Aya_English;
                else
                    previousAya = _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (firstAya - 1)).Aya;

                previousAya += $"({firstAya - 1})";
            }
            //1- ngeb 3dd el ayat ele fe el sora id dh (firstAyaSoraId)
            nextAya += $"({lastAya})";
            if (langCode == "en"&&!nomoreAyat)
                nextAya += _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (lastAya + 1)).Aya_English == null
                    ? _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (lastAya + 1)).Aya
                    : _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (lastAya + 1)).Aya_English;
            else
            {
                if(!nomoreAyat)
                    nextAya += _DbContext.QuranAyats.FirstOrDefault(x => x.SoraID == firstAyaSoraId && x.AyaNum == (lastAya + 1)).Aya;
            }


            //int lastAyaSoraId = _DbContext.QuranAyats.Find(lastAya).SoraID.Value;
            //if (lastAyaSoraId != 0 && _DbContext.QuranSuars.Where(x => x.ID == lastAyaSoraId).Select(e => e.QuranAyats).Count() > lastAya)
            //{
            //    nextAyaWords =  _DbContext.QuranWords.Where(x => (x.AyaNum == lastAya + 1)&&x.SoraID==lastAyaSoraId).Select(x => x.Word).ToList();
            //}

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
            resultModel.Words = finalWords;
            resultModel.PreviousAyaWords = previousAya;
            resultModel.NextAyaWords = nextAya;
            return resultModel;
        }

        public Boolean AddSubscriber(string email)
        {
            if (email == null)
            {
                return false;
            }

            _DbContext.Subscribers.Add(new Subscriber() {Date = DateTime.Now, Email = email});
            _DbContext.SaveChanges();
            return true;

        }

    }
}

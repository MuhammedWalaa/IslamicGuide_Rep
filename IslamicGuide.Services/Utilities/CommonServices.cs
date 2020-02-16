using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private readonly IslamicCenterEntities _DbContext;
        private readonly SubjectService _subjectService;
        public CommonServices()
        {
            _subjectService = new SubjectService();
            _DbContext = new IslamicCenterEntities();
        }
        public KeyValueList GetQuranWords(int from, int to, int ayatCount,string langCode)
        {
            KeyValueList res = new KeyValueList(){};
            
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

            res.Words = finalWords;
            res.FirstAya = ayatRange.First().Value;
            res.LastAya = ayatRange.Last().Value;
            return res;
        }

        public List<Tab> GetBooksTabs()
        {
            var tabs = _DbContext.BookCategories.Select(s => new Tab()
            {
                Category = s.Category,
                Id = s.ID,
                EnglishCategory = s.Category_English
            }).ToList();
            return tabs;
        }
        public PositionWordsWithNextAndPrevModel GetQuranWordsWithNextPrevAyah(int posId ,  string langCode)
        {
            #region Vars
            PositionWordsWithNextAndPrevModel resultModel = new PositionWordsWithNextAndPrevModel();
            string previousAyaText = "";
            string nextAyaText = "";
            string positionAyah = "";
            var position = _DbContext.Positions.Find(posId);
            var posAyahFrom = _DbContext.QuranWords.FirstOrDefault( x=>x.ID  ==  position.FromQuranWordID); 
            var posAyahTo = _DbContext.QuranWords.FirstOrDefault( x=>x.ID  ==  position.ToQuranWordID); 


            var ayaIdFrom = posAyahFrom.AyaID;
            var ayaIdTo = posAyahTo.AyaID;
            var nextAya = new AyatView();
            var prevAya = new AyatView();
            #endregion
            #region Logic
            if (ayaIdFrom == ayaIdTo)
            {
                var ayah = _DbContext.AyatViews.FirstOrDefault(x => x.AyaID == ayaIdFrom);
                positionAyah = ayah.AyaWords + " ( " + ayah.AyaNum + " ) ";
                nextAya = _DbContext.AyatViews.FirstOrDefault(x => x.SoraID == ayah.SoraID && x.AyaNum == ayah.AyaNum + 1);
                prevAya = _DbContext.AyatViews.FirstOrDefault(x => x.SoraID == ayah.SoraID && x.AyaNum == ayah.AyaNum - 1);
                resultModel.AyahNumbers = "1";
            }
            else
            {
                var ayahFrom = _DbContext.AyatViews.FirstOrDefault(x => x.AyaID == ayaIdFrom);
                var ayahTo = _DbContext.AyatViews.FirstOrDefault(x => x.AyaID == ayaIdTo);
                var ayahCount = ayahTo.AyaNum - ayahFrom.AyaNum;
                if (ayahCount == 1)
                {
                    positionAyah = ayahFrom.AyaWords + " ( " + ayahFrom.AyaNum + " ) " + ayahTo.AyaWords + " ( " + ayahTo.AyaNum + " ) ";
                   
                }
                else
                {

                    for (int i = 0; i < ayahCount + 1; i++)
                    {
                        var current = _DbContext.AyatViews.FirstOrDefault(x => x.SoraID == ayahFrom.SoraID && x.AyaNum == ayahFrom.AyaNum + i);
                        positionAyah += current.AyaWords + " ( " + current.AyaNum + " ) ";
                    }
                }
                resultModel.AyahNumbers = (ayahCount + 1).ToString();
                nextAya = _DbContext.AyatViews.FirstOrDefault(x => x.SoraID == ayahTo.SoraID && x.AyaNum == ayahTo.AyaNum + 1);
                prevAya = _DbContext.AyatViews.FirstOrDefault(x => x.SoraID == ayahFrom.SoraID && x.AyaNum == ayahFrom.AyaNum - 1);

            }

            #endregion
            resultModel.PositionAyah = positionAyah;
            resultModel.PreviousAyaWords = prevAya == null ? "" : prevAya.AyaWords + " ( " + prevAya.AyaNum + " ) "; 
            resultModel.NextAyaWords = nextAya == null ? "" : nextAya.AyaWords + " ( " + nextAya.AyaNum + " ) "; 
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

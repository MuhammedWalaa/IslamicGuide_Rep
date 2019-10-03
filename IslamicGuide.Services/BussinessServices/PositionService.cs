using IslamicGuide.Services.Utilities;
using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Position;
using IslamicGuide.Data.ViewModels.Shared;
using IslamicGuide.Data.ViewModels.Subjects;

namespace IslamicGuide.Services.BussinessServices
{

    public class PositionService
    {

        private readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        private readonly CommonServices _commonServices;
        public PositionService()
        {
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
            _commonServices = new CommonServices();
        }

        public PageListResult<PositionVM> AdjustingPositionData(PageFilterModel filter, int id)
        {
            List<PositionVM> positions = GetSubjectAndSubSubjectPositionsById(id, filter.LangCode);
            int positionsCount = positions.Count;

            return new PageListResult<PositionVM>()
            {
                RowsCount = positionsCount,
                DataList = positions.Skip(filter.Skip)
                    .Take(filter.PageSize).ToList(),

            };
        }
        public List<BookContentVM> GetPositionSpecificContent(int positionId,string langCode, int bookId)
        {
            var content = _DbContext.MapBookQurans.Where(m => m.BookID == bookId && m.PositionID == positionId).Select(
                e => new BookContentVM
                {
                    Title = langCode == "en" && e.Book.Title_English != null ? e.Book.Title_English : e.Book.Title,
                    ContentHTML = langCode == "en" && e.BookContent.BookContentHTML_English != null ? e.BookContent.BookContentHTML_English : e.BookContent.BookContentHTML,
                    BookId = e.BookID,
                }).FirstOrDefault();
            List<BookContentVM> Contents = new List<BookContentVM>();
            if (content != null)
                Contents.Add(content);
            return Contents;
        }

        public Title GetSubjectTitleForPosition(int positionId)
        {
            var positionSubject = _DbContext.MapSubjectsQurans.FirstOrDefault(p => p.PositionID == positionId);
            return new Title()
            {
                ArabicName = positionSubject.Subject.Title,
                EnglishName = positionSubject.Subject.Title_English
            };
        }
        public List<BookContentVM> GetPositionContent(int id, int? tabId, string langCode, bool isFromList)
        {
            if (isFromList)
            {
                tabId = _DbContext.Books.FirstOrDefault(x => x.ID == tabId).CategoryID;
            }
            //id refers to PositionId , tabId referes to CategoryId
            //return a list of book contents
            //if first time visit we go to first tab (Tafsir)
            BookContentVM bookContent = new BookContentVM();
            return _DbContext.MapBookQurans.Where(x => x.PositionID == id && x.Book.CategoryID == tabId).Select(e => new BookContentVM
            {
                Title = langCode == "en" && e.Book.Title_English != null ? e.Book.Title_English : e.Book.Title,
                ContentHTML = langCode == "en" && e.BookContent.BookContentHTML_English != null ? e.BookContent.BookContentHTML_English : e.BookContent.BookContentHTML,
                BookId = e.BookID,
            }).ToList();

            //var positionBookContents = _DbContext.MapBookQurans.Where(p => p.PositionID == e /*id*/).ToList();

        }
        public List<PositionVM> GetSubjectAndSubSubjectPositionsById(int id, string langCode)
        {
            List<PositionVM> positionVMs = new List<PositionVM>();
            int ayatCount = 0;

            if (id != 0)
            {
                var posIDs = _DbContext.MapSubjectsQurans.Where(p => p.SubjectID == id).Select(x => x.PositionID).ToList();
                //var strings = string.Join(" ", posIDs);
                var positions = _DbContext.Positions.Where(p => posIDs.Contains(p.ID)).ToList();
                foreach (var item in positions)
                {
                    string finalResult = "";
                    string suraTitle = "";
                    ayatCount = (item.QuranWord1.AyaID.Value - item.QuranWord.AyaID.Value) + 1;
                    if (ayatCount < 0)
                        ayatCount = (ayatCount * -1) + 1;
                    else
                    {
                        if (langCode == "en")
                            suraTitle = _DbContext.QuranSuars.Where(p => p.ID == item.QuranWord.SoraID).Select(e => e.Title_English == null ? e.Title : e.Title_English).FirstOrDefault();
                        else
                            suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == item.QuranWord.SoraID).Title;
                        var res = _commonServices.GetQuranWords(item.FromQuranWordID.Value, item.ToQuranWordID.Value, ayatCount, langCode);
                        var words_ayat = string.Join(" ", res);
                        if (ayatCount > 2)
                        {
                            var index = words_ayat.IndexOf(")", words_ayat.IndexOf(")") + 1) + 1;
                            finalResult = words_ayat.Substring(0, index);
                        }

                        else
                            finalResult = words_ayat.Substring(0, words_ayat.IndexOf(")") + 1);
                        if (finalResult == "")
                            positionVMs.Add(new PositionVM { PosID = item.ID, SuraNum = item.QuranWord.SoraID.Value, SuraTitle = suraTitle, AyatCount = ayatCount, QuranWords = words_ayat, From = item.FromQuranWordID.Value, To = item.ToQuranWordID.Value });
                        else
                            positionVMs.Add(new PositionVM { PosID = item.ID, SuraNum = item.QuranWord.SoraID.Value, SuraTitle = suraTitle, AyatCount = ayatCount, QuranWords = finalResult, From = item.FromQuranWordID.Value, To = item.ToQuranWordID.Value });

                    }
                }
                return positionVMs.OrderBy(x => x.SuraNum).ToList();

            }
            return null;

        }

        public PositionDetialsVM GetPositionDetials(int id, string langCode)
        {
            string title = "";
            string suraTitle = "";
            if (langCode == "en")
            {
                title = _DbContext.Subjects.Where(x => x.ID == id).Select(e => e.Title_English == null ? e.Title : e.Title_English).FirstOrDefault();
                //title = _DbContext.Subjects.Where(x => x.ID == x.MapSubjectsQurans.FirstOrDefault(e => e.ID == id).SubjectID).Select(s => s.Title_English == null ? s.Title : s.Title_English).FirstOrDefault();
            }
            else
                title = _DbContext.Subjects.FirstOrDefault(x => x.ID == id) == null ?
                    "." :
                    _DbContext.Subjects.FirstOrDefault(x => x.ID == id).Title;
            //title = _DbContext.Subjects.FirstOrDefault(x => x.ID == x.MapSubjectsQurans.FirstOrDefault(e => e.PositionID == id).SubjectID).Title;
            var position = _DbContext.Positions.FirstOrDefault(p => p.ID == id);
            if (position == null)
                return null;
            var ayatCount = (position.QuranWord1.AyaID.Value - position.QuranWord.AyaID.Value) + 1;
            if (ayatCount < 0)
                ayatCount = (ayatCount * -1) + 1;
            else
            {
                if (langCode == "en")
                    suraTitle = _DbContext.QuranSuars.Where(p => p.ID == position.QuranWord.SoraID).Select(e => e.Title_English == null ? e.Title : e.Title_English).FirstOrDefault();
                else
                    suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == position.QuranWord.SoraID).Title;
                var res = _commonServices.GetQuranWordsWithNextPrevAyah(position.FromQuranWordID.Value, position.ToQuranWordID.Value, ayatCount, langCode);

                var words_ayat = string.Join(" ", res.Words);

                return new PositionDetialsVM() { SuraTitle = suraTitle, NextAyaWords = res.NextAyaWords, PrevAyaWords = res.PreviousAyaWords, PositionQuranWords = words_ayat, Title = title };
            }

            return null;

        }
    }
}

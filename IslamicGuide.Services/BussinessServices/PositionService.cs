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

        private readonly IslamicCenterEntities _DbContext;
        private readonly CommonServices _commonServices;
        public PositionService()
        {
            _DbContext = new IslamicCenterEntities();
            _commonServices = new CommonServices();
        }

        public List<BookContentVM> GetBooksByCategory(int id, string lang)
        {
            var booksList = new List<BookContentVM>();
            var books = _DbContext.Books.Where(x => x.CategoryID == id).ToList();
            foreach (var book in books)
            {
                booksList.Add(
                    new BookContentVM()
                    {
                        BookId = book.ID,
                        Title = book.Title,
                        Title_English = book.Title_English,
                    });
            }

            return booksList;
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
        public List<BookContentVM> GetPositionSpecificContent(int positionId, string langCode, int bookId)
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

        public SubjectVM GetSubjectTitleForPosition(int positionId)
        {
            var positionSubject = _DbContext.MapSubjectsQurans.FirstOrDefault(p => p.PositionID == positionId);
            return new SubjectVM()
            {
                ID = positionSubject.SubjectID,
                Title_English = positionSubject.Subject !=null? positionSubject.Subject.Title_English:"",
                Title = positionSubject.Subject !=null? positionSubject.Subject.Title:""
            };
        }
        public List<BookContentVM> GetPositionContent(int id, int? tabId, string langCode, bool isFromList)
        {

            var bookid = tabId;
            if (isFromList)
            {
                tabId = _DbContext.Books.FirstOrDefault(x => x.ID == tabId).CategoryID;
            }

            //id refers to PositionId , tabId referes to CategoryId
            //return a list of book contents
            //if first time visit we go to first tab (Tafsir)
            var position = _DbContext.Positions.Find(id);
            var posAyahFrom = _DbContext.QuranWords.FirstOrDefault(x => x.ID == position.FromQuranWordID);
            var posAyahTo = _DbContext.QuranWords.FirstOrDefault(x => x.ID == position.ToQuranWordID);


            var ayaIdFrom = posAyahFrom.AyaNum;
            var ayaIdTo = posAyahTo.AyaNum;
            var suraId = posAyahFrom.SoraID;
            var lastAyaInSura = _DbContext.QuranAyats.Where(x => x.SoraID == suraId).OrderByDescending(x=>x.AyaNum).FirstOrDefault().AyaNum;
            if (ayaIdFrom != 1)
                ayaIdFrom--;
            if (ayaIdTo != lastAyaInSura)
                ayaIdTo++;
            string contentHTML = "";
            for (int i = ayaIdFrom.Value; i <= ayaIdTo; i++)
            {
                var ayaContent = "";
                ayaContent = langCode=="en" && _DbContext.QuranAyats.FirstOrDefault(x => x.AyaNum == i && x.SoraID == suraId).Aya_English !=null ? _DbContext.QuranAyats.FirstOrDefault(x => x.AyaNum == i && x.SoraID == suraId).Aya_English : _DbContext.QuranAyats.FirstOrDefault(x => x.AyaNum == i && x.SoraID == suraId).Aya;
                ayaContent= "<p style='display:inline; color:#d2983d'>" + ayaContent + "</p>";
                var newContent = _DbContext.BookContents.FirstOrDefault(x =>
                        x.SouraId == suraId && x.AyahId == i && x.BookId == bookid)?.BookContent1;
                if (newContent != null)
                {
                    newContent = ayaContent + ": " + newContent;
                    contentHTML += newContent+ "</br>" ;
                }
            }
            List<BookContentVM> bookContent = new List<BookContentVM>();
            var booktitle =_DbContext.Books.Find(bookid)?.Title;
            bookContent.Add(new BookContentVM()
            {
                Title = booktitle,
                BookId = bookid.Value,
                ContentHTML = contentHTML,
            });
            return bookContent;
            //return _DbContext.MapBookQurans.Where(x => x.PositionID == id && x.Book.ID == bookid).Select(e => new BookContentVM
            //{
            //    Title = langCode == "en" && e.Book.Title_English != null ? e.Book.Title_English : e.Book.Title,
            //    ContentHTML = langCode == "en" && e.BookContent.BookContentHTML_English != null ? contentHTML : contentHTML,
            //    BookId = e.BookID,
            //}).ToList();

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
                var positions = _DbContext.Positions.Where(p => posIDs.Contains(p.ID)).OrderBy(x=>x.QuranWord.AyaID).ToList();
                foreach (var item in positions)
                {
                    string finalResult = "";
                    string suraTitle = "";

                    var posAyahFrom = _DbContext.QuranWords.FirstOrDefault(x => x.ID == item.FromQuranWordID);

                    
                    ayatCount = (item.QuranWord1.AyaID.Value - item.QuranWord.AyaID.Value) + 1;
                    if (ayatCount < 0)
                        ayatCount = (ayatCount * -1) + 1;
                    else
                    {
                        suraTitle = langCode == "en"
                            ? posAyahFrom.QuranSuar.Title_English
                            : posAyahFrom.QuranSuar.Title;
                        var result = _commonServices.GetQuranWordsWithNextPrevAyah(item.ID, langCode);

                        var words_ayat = result.PositionAyah;
                       
                        
                        positionVMs.Add(new PositionVM { AyaNumbers = result.AyahNumbers, PosID = item.ID, SuraNum = posAyahFrom.QuranSuar.p_Id, SuraTitle = suraTitle, AyatCount = ayatCount, QuranWords = words_ayat, From = item.FromQuranWordID.Value, To = item.ToQuranWordID.Value, });
                        
                    }
                }
                return positionVMs.OrderBy(x => x.SuraNum).ThenBy(x=>x.From).ToList();

            }
            return null;

        }

        public PositionDetialsVM GetPositionDetials(int id, string langCode)
        {
            // Get Position 
            string suraTitle = "";

            #region Vars
            var myPosition = _DbContext.Positions.Find(id);

            if (myPosition == null)
                return new PositionDetialsVM();

            var posAyahFrom = _DbContext.QuranWords.FirstOrDefault(x => x.ID == myPosition.FromQuranWordID);

            suraTitle = langCode == "en" && posAyahFrom.QuranSuar.Title_English!=null
                ? posAyahFrom.QuranSuar.Title_English
                : posAyahFrom.QuranSuar.Title;
            #endregion

            var res = _commonServices.GetQuranWordsWithNextPrevAyah(id, langCode);
            var words_ayat = res.PositionAyah;
            return new PositionDetialsVM() { SuraTitle = suraTitle, NextAyaWords = res.NextAyaWords, PrevAyaWords = res.PreviousAyaWords, PositionQuranWords = words_ayat, Title = "" };


        }
    }
}

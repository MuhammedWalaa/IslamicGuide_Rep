using IslamicGuide.Services.Utilities;
using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Position;

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
        public List<PositionVM> GetSubjectAndSubSubjectPositionsById(int id,string langCode)
        {
            List<PositionVM> positionVMs = new List<PositionVM>();
            int ayatCount = 0;

            if (id != 0)
            {
                var posIDs = _DbContext.MapSubjectsQurans.Where(p => p.SubjectID == id).Select(x => x.ID).ToList();
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
                        if(langCode=="en")
                            suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == item.QuranWord.SoraID).Title_English;
                        else
                            suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == item.QuranWord.SoraID).Title;
                        var res = _commonServices.GetQuranWords(item.FromQuranWordID.Value, item.ToQuranWordID.Value, ayatCount,langCode);
                        var words_ayat = string.Join(" ", res);
                        if (ayatCount > 2)
                        {
                            var index = words_ayat.IndexOf(")", words_ayat.IndexOf(")")+1)+1;
                            finalResult = words_ayat.Substring(0,index);
                        }

                        else
                            finalResult = words_ayat.Substring(0, words_ayat.IndexOf(")") + 1);
                        if (finalResult=="")
                            positionVMs.Add(new PositionVM { PosID = item.ID, SuraNum = item.QuranWord.SoraID.Value, SuraTitle = suraTitle, AyatCount = ayatCount, QuranWords = words_ayat,From=item.FromQuranWordID.Value,To=item.ToQuranWordID.Value });
                        else
                            positionVMs.Add(new PositionVM { PosID = item.ID, SuraNum = item.QuranWord.SoraID.Value, SuraTitle = suraTitle, AyatCount = ayatCount, QuranWords = finalResult, From = item.FromQuranWordID.Value, To = item.ToQuranWordID.Value });

                    }
                }
                return positionVMs.OrderBy(x => x.SuraNum).ToList();

            }
            return null;

        }

        public PositionDetialsVM GetPositionDetials(int id,string langCode)
        {
            string title = "";
            string suraTitle = "";
            if(langCode=="en")
                title = _DbContext.Subjects.FirstOrDefault(x => x.ID == x.MapSubjectsQurans.FirstOrDefault(e => e.PositionID == id).SubjectID).Title_English;
            else
                title = _DbContext.Subjects.FirstOrDefault(x=>x.ID==x.MapSubjectsQurans.FirstOrDefault(e=>e.PositionID==id).SubjectID).Title;
            var position = _DbContext.Positions.FirstOrDefault(p => p.ID==id);
                var ayatCount = (position.QuranWord1.AyaID.Value - position.QuranWord.AyaID.Value) + 1;
                if (ayatCount < 0)
                    ayatCount = (ayatCount * -1) + 1;
                else
                {
                if(langCode=="en")
                    suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == position.QuranWord.SoraID).Title_English;
                else
                    suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == position.QuranWord.SoraID).Title;
                var res = _commonServices.GetQuranWords(position.FromQuranWordID.Value, position.ToQuranWordID.Value, ayatCount, langCode);
                    var words_ayat = string.Join(" ", res);
                return new PositionDetialsVM() { SuraTitle = suraTitle, PositionQuranWords = words_ayat,Title= title };
                }
            
            return null;

        }
    }
}

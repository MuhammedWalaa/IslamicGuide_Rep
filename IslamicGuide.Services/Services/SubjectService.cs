using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Position;
using IslamicGuide.Data.ViewModels.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Services.Services
{

    public class SubjectService
    {
        private readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        public SubjectService()
        {
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
        }
        public int CountSubjects()
        {
            
            return _DbContext.Subjects.Where(x => x.ParentID == 1).Count();

        }
        public SubjectVM GetSubjectById(int id)
        {
            SubjectVM subject = new  SubjectVM();

            var subj = _DbContext.Subjects.Where(p => p.ID==id).FirstOrDefault();
            subject.ID = subj.ID;
            subject.Title = subj.Title;
            return subject;
        }
        public int GetSubjectParentId(int id)
        {

            var parentId = _DbContext.Subjects.Where(x => x.ID == id).FirstOrDefault().ParentID;
            if (parentId != null)
                return parentId.Value;
            return 0;
        }
        public List<SubjectVM> GetMainSubjects()
        {
            List<SubjectVM> subjects = new List<SubjectVM>();
            var subjectList = _DbContext.Subjects.Where(p=>p.ParentID==1).ToList();
            foreach (var item in subjectList)
            {
                subjects.Add(new SubjectVM {ID = item.ID, Title = item.Title });
            }
            //subjects
            return subjects;
        }
        public List<SubjectVM> GetSubSubjectById(int id)
        {
            List<SubjectVM> subSubjects = new List<SubjectVM>();
            if (id != 0)
            {
                var subSubjectList = _DbContext.Subjects.Where(x => x.ParentID == id).ToList();
                var parentTitle = subjectTitle(id);


                    foreach (var item in subSubjectList)
                    {
                        subSubjects.Add(new SubjectVM { ParentTitle = parentTitle, ID = item.ID, Title = item.Title });
                    }
                
            }
            //subjects
            return subSubjects;
        }
        public string subjectTitle(int id)
        {
            if(id!=0)
                return _DbContext.Subjects.Where(x => x.ID == id).FirstOrDefault().Title;
            return "";
        }
        private Dictionary<int,string> GetQuranWords(int from , int to,int ayatCount)
        {
            List<string> lastWords = new List<string>();
            List<int> ayatDistinct = new List<int>();
            Dictionary<int, string> word_Aya = new Dictionary<int, string>();
            var allWords = _DbContext.QuranWords.OrderBy(e => e.p_Id).Skip(from-1).Take(to+1 - from).Select(x => new { x.Word, x.AyaNum ,x.p_Id}).ToList();
            var c = allWords.Select(x => x.Word).ToList();
            //if (ayatCount > 1)
            //{
            //    ayatDistinct = _DbContext.QuranWords.OrderBy(e => e.p_Id).Skip(from).Take(to - from).Select(x => x.AyaNum.Value).Distinct().ToList();
            //    foreach (var item in ayatDistinct)
            //    {
            //        lastWords.Add(_DbContext.QuranWords.OrderByDescending(e => e.p_Id).FirstOrDefault(w => w.AyaNum == item && allWords.Contains(w.Word)).Word);
            //    }
            //    for (int i = 0; i < ayatDistinct.Count(); i++)
            //    {
            //        word_Aya.Add(ayatDistinct[i], lastWords[i]);
            //    }
            //    word_Aya.Add(0, " ");
            //}
            //else
            //{
                ayatDistinct = _DbContext.QuranWords.OrderBy(e => e.p_Id).Skip(from).Take(to - from).Select(x => x.AyaNum.Value).Distinct().ToList();
            for (int i = 0; i < ayatDistinct.Count()-1; i++)
            {
                int ayah = ayatDistinct[i];
                //lastWords.Add(_DbContext.QuranWords.OrderByDescending(e => e.p_Id).FirstOrDefault(w => w.AyaNum == ayah && allWords.Contains(w.Word)).Word);

            }
        
                //ayatDistinct.Add(_DbContext.QuranWords.FirstOrDefault(e => e.p_Id == from).AyaNum.Value);
                //lastWords.Add(_DbContext.QuranWords.FirstOrDefault(e=>e.p_Id==to).Word);
                word_Aya.Add(ayatDistinct[0], lastWords[0]);
                word_Aya.Add(0," ");
            //}
            //var finalWords = string.Join(" ", allWords);
            for (int i = 0; i < lastWords.Count(); i++)
            {
                //var ss = allWords.IndexOf(lastWords[i]);
                //allWords[allWords.LastIndexOf(lastWords[i])]+="("+ayatDistinct[i]+")";

                //var x = finalWords.IndexOf(lastWords[i]);
                //finalWords.Insert(finalWords.LastIndexOf(lastWords[i]), "(" + ayatDistinct[i].ToString() + ")");
            }
            //finalWords.LastIndexOf(word_Aya[wor)
            //finalWords.Insert()
            //var lastWords = _DbContext.QuranWords.LastOrDefault(q => ayatDistinct.Contains(q.AyaNum)).Word;
            return word_Aya;
        }
        public List<PositionVM> GetSubSubjectPositionsById(int id)
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

                    ayatCount = (item.QuranWord1.AyaNum.Value - item.QuranWord.AyaNum.Value)+1;
                    if (ayatCount < 0)
                        ayatCount = (ayatCount * -1) + 1;
                    else
                    {
                        var suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == item.QuranWord.SoraID).Title;
                        var word_Aya = GetQuranWords(item.FromQuranWordID.Value, item.ToQuranWordID.Value, ayatCount);

                        var words = string.Join("(" + word_Aya.Values.ToString() + ")", word_Aya.Keys);
                        positionVMs.Add(new PositionVM { PosID = item.ID, SuraNum = item.QuranWord.SoraID.Value, SuraTitle = suraTitle, AyatCount = ayatCount + 1, QuranWords = words });
                    }
                }
                return positionVMs.OrderBy(x=>x.SuraNum).ToList();
                //return _DbContext.Positions.Where(p => posIDs.Contains(p.ID)).Select(x => new PositionVM() {
                //    PosID = x.ID,
                //    SuraNum = x.QuranWord.SoraID.Value,
                //    AyatCount = (x.QuranWord1.AyaNum.Value - x.QuranWord.AyaNum.Value) >= 0 ? (x.QuranWord1.AyaNum.Value - x.QuranWord.AyaNum.Value) + 1 : ((x.QuranWord1.AyaNum.Value - x.QuranWord.AyaNum.Value) * -1) + 1,
                //    SuraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == x.QuranWord.SoraID).Title,
                //    QuranWords = _DbContext.QuranWords.Where(e => e.p_Id == x.FromQuranWordID).Take(x.FromQuranWordID.Value - x.ToQuranWordID.Value).Select(q => q.Word).ToList(),
                //    //QuranWords = _DbContext.QuranWords.Where(e=>Enumerable.Range(x.FromQuranWordID.Value,(x.ToQuranWordID.Value-x.FromQuranWordID.Value)).Contains(e.p_Id)).Select(e=>e.Word).ToList(),
                //}).OrderBy(e => e.SuraNum).ToList();

                //foreach (var item in SubSubjectPositions)
                //{

                //    ayatCount = item.QuranWord1.AyaNum.Value - item.QuranWord.AyaNum.Value;
                //    if (ayatCount >= 0)
                //    {
                //        var suraTitle = _DbContext.QuranSuars.FirstOrDefault(p => p.ID == item.QuranWord.SoraID).Title;
                //        positionVMs.Add(new PositionVM { PosID = item.ID, SuraNum = item.QuranWord.SoraID.Value, SuraTitle = suraTitle, AyatCount = ayatCount + 1 });
                //    }
                //}
                //foreach (var item in subSubjectList)
                //{
                //    //subSubjects.Add(new SubjectVM { ID = item.ID, Title = item.Title });
                //
            }
            return null;
                //Positions
                //return positionVMs.OrderBy(x=>x.SuraNum).ToList();
        }
        public List<SubjectVM> GetAllSubjects()
        {
            List<SubjectVM> subjects = new List<SubjectVM>();
            return _DbContext.Subjects.Where(p => p.ParentID == 1).Select(x => new SubjectVM() {
                ID = x.ID,
                Title = x.Title,
            }).ToList();
            //foreach (var item in subjectslist)
            //{
            //    subjects.Add(new SubjectVM { ID = item.ID, Title = item.Title });
            //}
            //return subjects;
        }
        
    }
}

using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Position;
using IslamicGuide.Data.ViewModels.Subjects;
using IslamicGuide.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IslamicGuide.Services.BussinessServices
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

        public SubjectVM GetSubjectById(int id,string langCode)
        {
            SubjectVM subject = new SubjectVM();

            Subject subj = _DbContext.Subjects.Where(p => p.ID == id).FirstOrDefault();
            subject.ID = subj.ID;
            subject.Title = langCode=="en"? subj.Title_English:subj.Title;
            return subject;
        }
        public int GetSubjectParentId(int id)
        {

            int? parentId = _DbContext.Subjects.Where(x => x.ID == id).FirstOrDefault().ParentID;
            if (parentId != null)
            {
                return parentId.Value;
            }

            return 0;
        }
        public List<SubjectVM> GetMainSubjects(string langCode)
        {
            List<SubjectVM> subjects = new List<SubjectVM>();
            List<Subject> subjectList = _DbContext.Subjects.Where(p => p.ParentID == 1).ToList();
            foreach (Subject item in subjectList)
            {
                subjects.Add(new SubjectVM { ID = item.ID, Title = langCode=="en"?item.Title_English:item.Title});
            }
            //subjects
            return subjects;
        }
        public List<SubjectVM> GetSubSubjectById(int id,string langCode)
        {
            List<SubjectVM> subSubjects = new List<SubjectVM>();
            if (id != 0)
            {
                
                List<Subject> subSubjectList = _DbContext.Subjects.Where(x => x.ParentID == id).ToList();
                string parentTitle = subjectTitle(id,langCode);


                foreach (Subject item in subSubjectList)
                {
                    subSubjects.Add(new SubjectVM { ParentTitle = parentTitle, ID = item.ID, Title = langCode == "en" ? item.Title_English : item.Title });
                }

            }
            //subjects
            return subSubjects;
        }
        public string subjectTitle(int id,string langCode)
        {
            if (id != 0)
            {
                if(langCode=="en")
                    return _DbContext.Subjects.Where(x => x.ID == id).FirstOrDefault().Title_English;
                else
                    return _DbContext.Subjects.Where(x => x.ID == id).FirstOrDefault().Title;
            }

            return "";
        }
        
        public List<SubjectVM> GetAllSubjects(string langCode)
        {
            List<SubjectVM> subjects = new List<SubjectVM>();
            var subjectslist = _DbContext.Subjects.Where(p=>p.ParentID==1).ToList();
            foreach (var item in subjectslist)
            {
                subjects.Add(new SubjectVM { ID = item.ID, Title = langCode == "en" ? item.Title_English : item.Title });
            }
            return subjects;
        }

    }
}

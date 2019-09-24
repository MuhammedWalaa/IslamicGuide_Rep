using IslamicGuide.Data;
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
                var parentTitle = _DbContext.Subjects.Where(x => x.ID == id).FirstOrDefault().Title;
                
                    foreach (var item in subSubjectList)
                    {
                        subSubjects.Add(new SubjectVM { ParentTitle = parentTitle, ID = item.ID, Title = item.Title });
                    }
                
            }
            //subjects
            return subSubjects;
        }
        public List<SubjectVM> GetSubSubjectPositionById(int id)
        {
            List<SubjectVM> subSubjects = new List<SubjectVM>();
            if (id != 0)
            {
                var subSubjectList = _DbContext.Subjects.Where(x => x.ParentID == id).ToList();
                foreach (var item in subSubjectList)
                {
                    subSubjects.Add(new SubjectVM { ID = item.ID, Title = item.Title });
                }
            }
            //subjects
            return subSubjects;
        }
        public List<SubjectVM> GetAllSubjects()
        {
            List<SubjectVM> subjects = new List<SubjectVM>();
            var subjectslist = _DbContext.Subjects.Where(p=>p.ParentID==1).ToList();
            foreach (var item in subjectslist)
            {
                subjects.Add(new SubjectVM { ID = item.ID, Title = item.Title });
            }
            return subjects;
        }
        
    }
}

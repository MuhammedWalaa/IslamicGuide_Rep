using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Position;
using IslamicGuide.Data.ViewModels.Shared;
using IslamicGuide.Data.ViewModels.Subjects;
using IslamicGuide.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IslamicGuide.Services.BussinessServices
{

    public class SubjectService
    {
        private readonly IslamicCenterEntities _DbContext;

        public PageListResult<SubjectVM> SearchQuery(PageFilterModel pageFilterModel , string searchQuery)
        {
            var listOfSubjects = _DbContext.Subjects.Where(x => x.Title.Contains(searchQuery) || x.Title_English.ToLower().Contains(searchQuery))
                .Select(e=>new SubjectVM()
                {
                    ID = e.ID,
                    Title = e.Title,
                    Title_English = e.Title_English,
                    ParentTitle = e.Subject1!=null?e.Subject1.Title:null,
                }).ToList();
            if (listOfSubjects.Count == 0)
                return null;
            return new PageListResult<SubjectVM>()
            {
                RowsCount = listOfSubjects.Count,
                DataList = listOfSubjects.Skip(pageFilterModel.Skip)
                    .Take(pageFilterModel.PageSize).ToList(),
            };
        }

        public SubjectService()
        {
            _DbContext = new IslamicCenterEntities();
        }
        public int CountSubjects()
        {

            return _DbContext.Subjects.Count(x => x.ParentID == 1);

        }
        public PageListResult<SubjectVM> AdjustingMainSubjectsData(PageFilterModel pageFilterModel, int subjectId)
        {
            List<SubjectVM> subjects = GetSubSubjectById(subjectId,pageFilterModel.LangCode);
            if (subjects.Count == 0)
                return null;
            return new PageListResult<SubjectVM>()
            {
                RowsCount = subjects.Count,
                DataList = subjects.Skip(pageFilterModel.Skip)
                    .Take(pageFilterModel.PageSize).ToList(),
            };
        }
        
        public List<SubjectVM> GetDDLBySubjectParentId(int subjectParentId)
        {
            return _DbContext.Subjects.Where(x => x.ParentID == subjectParentId).Select(e => new SubjectVM()
            {
                Title = e.Title,
                Title_English = e.Title_English,
                ID = e.ID,
            }).ToList();
        }
        public SubjectVM GetSubjectById(int id,string langCode)
        {
            Subject subj = _DbContext.Subjects.Find(id);
            if (subj == null)
                return new SubjectVM();
            return  new SubjectVM()
            {
                ID = subj.ID,
                Title = subj.Title?? ".",
                Title_English = subj.Title_English??".",
                ParentTitle = subj.Subject1?.Title,

            };
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
            if (langCode == "en")
            {
                foreach (Subject item in subjectList)
                {
                    subjects.Add(new SubjectVM { ID = item.ID, Title = item.Title_English ==null?item.Title: item.Title_English});
                }
            }
            else
            {
                foreach (Subject item in subjectList)
                {
                    subjects.Add(new SubjectVM { ID = item.ID, Title = item.Title });
                }
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

                if(langCode == "en")
                {
                    foreach (Subject item in subSubjectList)
                    {
                        subSubjects.Add(new SubjectVM { ParentTitle = parentTitle, ID = item.ID, Title = item.Title_English==null?item.Title :item.Title_English});
                    }
                }
                else
                {
                    foreach (Subject item in subSubjectList)
                    {
                        subSubjects.Add(new SubjectVM { ParentTitle = parentTitle, ID = item.ID, Title = item.Title });
                    }
                }
            }
            //subjects
            return subSubjects;
        }
        public string subjectTitle(int id,string langCode)
        {
            if (id != 0)
            {
                if (langCode == "en")
                    return _DbContext.Subjects.Where(x => x.ID == id).Select(e => e.Title_English == null ? e.Title : e.Title_English).FirstOrDefault();
                else
                    return _DbContext.Subjects.FirstOrDefault(x => x.ID == id).Title;
            }
            return "";
        }
        
        public List<SubjectVM> GetAllSubjects(string langCode)
        {
            List<SubjectVM> subjects = new List<SubjectVM>();
            var subjectslist = _DbContext.Subjects.Where(p=>p.ParentID==1).ToList();
            if (langCode == "en")
            {
                foreach (var item in subjectslist)
                {
                    subjects.Add(new SubjectVM { ID = item.ID, Title = item.Title_English==null?item.Title :item.Title });
                }
            }
            else
            {
                foreach (var item in subjectslist)
                {
                    subjects.Add(new SubjectVM { ID = item.ID, Title = item.Title });
                }
            }
            return subjects;
        }

    }
}

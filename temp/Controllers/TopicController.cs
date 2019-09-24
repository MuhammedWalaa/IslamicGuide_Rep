using IslamicGuide.Data.ViewModels.Subjects;
using IslamicGuide.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{
    public class TopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        public TopicController()
        {
            _subjectService = new SubjectService();
        }
        // GET: Topic
        public ActionResult Index()
        {
            var subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }
        public ActionResult GetById(int id)
        {
            var subSubjects = _subjectService.GetSubSubjectById(id);
            if (subSubjects.Count() == 0)
            {
                return RedirectToAction("GetById", "SubTopic", new { id = id });
            }
            SubSubjectPageVM SubPage = new SubSubjectPageVM();
            List<SubjectVM> dropList = new List<SubjectVM>();
            var parentID = _subjectService.GetSubjectParentId(id);
            if (parentID == 0)
                dropList = null;
            else if (parentID == 1)
            {
                var mainsubjs = _subjectService.GetMainSubjects();
                dropList = mainsubjs;
            }
            else
            {
                var mainsubjs = _subjectService.GetSubjectById(id);
                dropList.Add(mainsubjs);
            }
            
            SubPage.subjectsDropdown = dropList;
            SubPage.subjectVM = subSubjects;
            return View(SubPage);
        }
    }
}

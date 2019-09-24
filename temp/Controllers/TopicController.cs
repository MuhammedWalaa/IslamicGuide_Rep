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
            List<SubjectVM> dropList = new List<SubjectVM>();
            if (id == 1)
            {
                var mainsubjs = _subjectService.GetMainSubjects();
                ViewBag.subj = mainsubjs;
            }
            else
            {
                var mainsubjs = _subjectService.GetSubjectById(id);
                dropList.Add(mainsubjs);
            }
            var subSubjects = _subjectService.GetSubSubjectById(id);
            return View(subSubjects);
        }
    }
}

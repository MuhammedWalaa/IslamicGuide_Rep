using IslamicGuide.Data.ViewModels.Subjects;
using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IslamicGuide.Services.Utilities;

namespace IslamicGuide.App.Controllers
{
    public class TopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly RouteService _routeService;
        public TopicController()
        {
            _routeService= new RouteService();
            _subjectService = new SubjectService();
        }
        // GET: Topic
        public ActionResult Index()
        {
            List<SubjectVM> subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }
        public ActionResult GetById(int id)
        {
            var currentRoute = _subjectService.GetSubjectById(id);
            _routeService.RouteHandling(currentRoute.Title,"Topic","GetById",id,Routes);

            List<SubjectVM> subSubjects = _subjectService.GetSubSubjectById(id);
            if (subSubjects.Count() == 0)
            {
                return RedirectToAction("GetById", "SubTopic", new { id = id });
            }
            SubSubjectPageVM SubPage = new SubSubjectPageVM();
            List<SubjectVM> dropList = new List<SubjectVM>();
            int parentID = _subjectService.GetSubjectParentId(id);
            if (parentID == 0)
            {
                dropList = null;
            }
            else if (parentID == 1)
            {
                List<SubjectVM> mainsubjs = _subjectService.GetMainSubjects();
                dropList = mainsubjs;
            }
            else
            {
                SubjectVM mainsubjs = _subjectService.GetSubjectById(id);
                dropList.Add(mainsubjs);
            }

            SubPage.subjectsDropdown = dropList;
            SubPage.subjectVM = subSubjects;
            return View(SubPage);
        }
    }
}

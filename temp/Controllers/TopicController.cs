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
        private readonly PositionService _positionService;

        public TopicController()
        {
            _routeService= new RouteService();
            _subjectService = new SubjectService();
            _positionService = new PositionService();
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
            var positions = _positionService.GetSubjectAndSubSubjectPositionsById(id);
            
            List<SubjectVM> subSubjects = _subjectService.GetSubSubjectById(id);
            if (subSubjects.Count() == 0)
            {
                return RedirectToAction("GetPositionsById", "Topic", new { id = id });
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
            if (positions.Count!=0)
            {
                SubPage.HasPosition = true;
            }
            else
            {
                SubPage.HasPosition = false;
            }
            SubPage.subjectsDropdown = dropList;
            SubPage.subjectVM = subSubjects;
            SubPage.Id = id;
            return View(SubPage);
        }
        public ActionResult GetPositionsById(int id)
        {
            var positions = _positionService.GetSubjectAndSubSubjectPositionsById(id);
            var parentTitle = _subjectService.subjectTitle(id);
            _routeService.RouteHandling(parentTitle, "SubTopic", "GetById", id, Routes);
            ViewBag.subjectTitle = parentTitle;
            return View(positions);
        }

        public ActionResult GetPositionsForSubject(int id)
        {
            return RedirectToAction("GetPositionsById", "Topic", new { id = id });
        }

    }

}

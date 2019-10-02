using IslamicGuide.Data.ViewModels.Subjects;
using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IslamicGuide.Services.Utilities;
using IslamicGuide.Data.ViewModels.Shared;

namespace IslamicGuide.App.Controllers
{
    public class TopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly RouteService _routeService;
        private readonly PositionService _positionService;

        public TopicController()
        {
            _routeService = new RouteService();
            _subjectService = new SubjectService();
            _positionService = new PositionService();
        }
        // GET: Topic
        public ActionResult Index(int ? page, int subjectId = 1)
        {
            
            var res = Search(subjectId,page);
            if (res == 1)
                return RedirectToAction("Index", "Positions", new { id = subjectId });
            else if (res == 2)
                return View("Error");
            var currentRoute = _subjectService.GetSubjectById(subjectId, LangCode);
            _routeService.RouteHandling(currentRoute.Title, "Topic", "Index", subjectId, Routes);

            return View("Index");
        }

        //public ActionResult GetPositionsById(int id)
        //{
        //    return RedirectToAction("Index", "Positions", new { id = id });
        //}

        public int Search(int subjectId, int? page)
        {
            if (subjectId != 0)
            {
                ViewBag.SubjectId = subjectId;
            }
            var pageUrl = "/topic";
            if (subjectId != 0)
            {
                pageUrl += "?SubjectId=" + subjectId;
            }
            var positions = _positionService.GetSubjectAndSubSubjectPositionsById(subjectId, LangCode);
            
            SubSubjectPageVM SubPage = new SubSubjectPageVM();
            List<SubjectVM> dropList = new List<SubjectVM>();
            int pageSize = 6;

            var result = _subjectService.AdjustingMainSubjectsData(new PageFilterModel()
            {
                LangCode = LangCode,
                PageSize = pageSize,
                Skip = ((page ?? 1) - 1) * pageSize
            }, subjectId);
            // if Subject has no SubSubjects
            if (result == null)
            {
                if(positions.Count==0)
                    return 2;
                return 1;
            }

            //Pagination Pages Count
            var pagesCount = 1;
            if (result.RowsCount % pageSize == 0)
                pagesCount = (result.RowsCount / pageSize);
            else
                pagesCount = (result.RowsCount / pageSize) + 1;



            int parentId = _subjectService.GetSubjectParentId(subjectId);

            if (parentId == 0)
            {
                List<SubjectVM> mainsubjs = _subjectService.GetMainSubjects(LangCode);
                dropList = mainsubjs;
            }
            else
            {
                SubjectVM mainsubjs = _subjectService.GetSubjectById(subjectId, LangCode);
                dropList.Add(mainsubjs);
            }



            SubPage.HasPosition = positions.Any() ? true : false;
            SubPage.subjectsDropdown = dropList;
            SubPage.DataList = result;
            SubPage.Id = subjectId;


            ViewBag.SubPage = SubPage;
            ViewBag.PagingResult = new PagingModel
            {
                CurrentPage = page ?? 1,
                Url = pageUrl,
                PagesCount = pagesCount
            };
            return 0;
        }
        public ActionResult GetByIdList(int id)
        {
            _routeService.PopRoutesOutOfIndexFromList(Routes, Routes[Routes.Count-1].Text);
            return RedirectToAction("Index", new { id = id });
        }

        public ActionResult GetById(int id,int? page)
        {
            List<SubjectVM> subSubjects = _subjectService.GetSubSubjectById(id, LangCode);
            if (subSubjects.Any())
            {
                return RedirectToAction("GetPositionsById", "Positions", new { id = id });
            }


            // Handling Routing
            var currentRoute = _subjectService.GetSubjectById(id,LangCode);
            _routeService.RouteHandling(currentRoute.Title,"Topic","GetById",id,Routes);


            var positions = _positionService.GetSubjectAndSubSubjectPositionsById(id,LangCode);
            
            SubSubjectPageVM SubPage = new SubSubjectPageVM();
            List<SubjectVM> dropList = new List<SubjectVM>();

            int parentID = _subjectService.GetSubjectParentId(id);
            if (parentID == 0)
            {
                dropList = null;
            }
            
            else if (parentID == 1)
            {
                List<SubjectVM> mainsubjs = _subjectService.GetMainSubjects(LangCode);
                dropList = mainsubjs;
            }
            else
            {
                SubjectVM mainsubjs = _subjectService.GetSubjectById(id,LangCode);
                dropList.Add(mainsubjs);
            }
            
            SubPage.HasPosition = positions.Any() ? true : false;
            SubPage.subjectsDropdown = dropList;
            SubPage.subjectVM = subSubjects;
            SubPage.Id = id;
            return View(SubPage);
        }
        
        public ActionResult GetPositionsForSubject(int id)
        {
            return RedirectToAction("GetPositionsById", "Topic", new { id = id });
        }

    }

}

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
        public ActionResult Index(int ? page, int subjectId)
        {
            var res = Search(subjectId,page);

            if (res == 1)
                return RedirectToAction("Index", "Positions", new { id = subjectId });
            else if (res == 2)
                return View("Error");

            var parentId = _subjectService.GetSubjectParentId(subjectId);
            var parent = _subjectService.GetSubjectById(parentId, LangCode);
            var currentRoute = _subjectService.GetSubjectById(subjectId, LangCode);
            ViewBag.ParentTitle = currentRoute.Title;
            if (subjectId != 1)
            {
                _routeService.RouteHandling(
                    Request.Url.OriginalString
                    , new Title()
                    {
                        ArabicName = parent.Title,
                        EnglishName = parent.Title_English
                    }
                    , currentRoute.Title_English
                    , currentRoute.Title
                    , "Topic", "Index"
                    , subjectId
                    , Routes);

            }
            return View("Index");
        }
        public ActionResult ErrorSearch()
        {
            return View();
        }
        public ActionResult SearchLayout(string searchQuery, int? page)
        {
            if(!string.IsNullOrEmpty(searchQuery))
            {
                int pageSize = 12;

                var result = _subjectService.SearchQuery(new PageFilterModel()
                {
                    LangCode = LangCode,
                    PageSize = pageSize,
                    Skip = ((page ?? 1) - 1) * pageSize
                }, searchQuery.ToLower());
                if (result != null)
                {
                    var pageUrl = "/topic/SearchLayout";
                    pageUrl += "?searchQuery=" + searchQuery;

                    //Pagination Pages Count
                    var pagesCount = 1;
                    if (result.RowsCount % pageSize == 0)
                        pagesCount = (result.RowsCount / pageSize);
                    else
                        pagesCount = (result.RowsCount / pageSize) + 1;

                    SubSubjectPageVM SubPage = new SubSubjectPageVM();

                    SubPage.DataList = result;
                    ViewBag.Search = "true";
                    ViewBag.SubPage = SubPage;
                    ViewBag.PagingResult = new PagingModel
                    {
                        CurrentPage = page ?? 1,
                        Url = pageUrl,
                        PagesCount = pagesCount
                    };
                    return View("Index");
                }
                else
                {
                    TempData["NoResult"] = "true";
                    TempData.Keep();
                }
            }

            return View("ErrorSearch");

        }
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
            int pageSize = 20;

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
                parentId = 1;
            dropList = _subjectService.GetDDLBySubjectParentId(subjectId);
            ViewBag.ParID = subjectId;
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
            _routeService.PopRoutesOutOfIndexFromList(Request.Url.OriginalString,Routes);
            return RedirectToAction("Index", new { subjectId = id });
        }
        public ActionResult PreIndex(int subjectId)
        {
            var currentRoute = _subjectService.GetSubjectById(1, LangCode);
            _routeService.RouteHandling(
                "http://localhost:52620/Topic?subjectId=1"
                , new Title()
                , currentRoute.Title_English
                , currentRoute.Title
                , "Topic", "Index"
                , subjectId
                , Routes);

            return RedirectToAction("Index", "Topic", new { subjectId = subjectId });
        }

    }

}

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
        public ActionResult Index(int? page, int subjectId)
        {
            string parentEnglishTitle = "";
            string parentArabicTitle = "";
            var res = Search(subjectId, page);

            if (res == 1)
                return RedirectToAction("Index", "Positions", new { id = subjectId });
            else if (res == 2)
                return View("Error");

            var parentId = _subjectService.GetSubjectParentId(subjectId);
            var parent = _subjectService.GetSubjectById(parentId);
            if (parentId == 1)
            {
                parentEnglishTitle = "A";
                parentArabicTitle = "الرئيسية";
            }
            else
            {
                parentEnglishTitle = parent.Title_English;
                parentArabicTitle = parent.Title;
            }

            var currentRoute = _subjectService.GetSubjectById(subjectId);
            if (parentId != 0)
            {
                if (parent.ID != 1)
                {
                    ViewBag.ParentImageName = parent.Title;

                    if (parent.ParentTitle != null && parent.ParentTitle != "التقسيم الموضوعي للقرآن الكريم")
                        ViewBag.ParentImageName = parent.ParentTitle;
                }
                else
                    ViewBag.ParentImageName = currentRoute.Title;
            }
            else
            {
                ViewBag.ParentImageName = null;
            }

            ViewBag.ParentTitle = null;
            //
            if (subjectId != 1)
            {

                Routes = _routeService.AddAllPreviousRoutesOfRequest(subjectId, Request.Url.OriginalString,"/");

                _routeService.RouteHandling(
                    Request.Url.OriginalString
                    , new Title()
                    {
                        ArabicName = parentArabicTitle,
                        EnglishName = parentEnglishTitle
                    }
                    , currentRoute.Title_English
                    , currentRoute.Title
                    , "Topic", "Index"
                    , subjectId
                    , Routes);

            }
            else
            {
                Routes = new List<Route>();
                _routeService.RouteHandling(Request.Url.OriginalString, null, "Home", "الرئيسية", "Home", "Index", null, Routes);
                
            }

            ViewBag.Routes = Routes;
            return View("Index");
        }
        public ActionResult ErrorSearch()
        {
            return View();
        }
        public ActionResult SearchLayout(string searchQuery, int? page)
        {
            List<string> searchedSubjectsImages = new List<string>();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                int pageSize = 20;

                var result = _subjectService.SearchQuery(new PageFilterModel()
                {
                    LangCode = LangCode,
                    PageSize = pageSize,
                    Skip = ((page ?? 1) - 1) * pageSize
                }, searchQuery.ToLower());
                if (result != null)
                {
                    foreach (var subjec in result.DataList)
                    {
                        string imageToAdd = "";
                        var parentId = _subjectService.GetSubjectParentId(subjec.ID);
                        var parent = _subjectService.GetSubjectById(parentId);
                        var currentRoute = _subjectService.GetSubjectById(subjec.ID);
                        if (parentId != 0)
                        {
                            if (parent.ID != 1)
                            {
                                while (parentId != 1)
                                {
                                    parentId = _subjectService.GetSubjectParentId(parent.ID);
                                    if (parentId != 1)
                                    {
                                        parent = _subjectService.GetSubjectById(parentId);
                                        imageToAdd = parent.Title;
                                    }
                                    else if (parentId ==1&&imageToAdd=="")
                                    {
                                        imageToAdd = currentRoute.ParentTitle;
                                    }
                                }
                                //if (parent.ParentTitle != null && parent.ParentTitle != "التقسيم الموضوعي للقرآن الكريم")
                                //    imageToAdd = parent.ParentTitle;
                            }
                            else
                                imageToAdd = currentRoute.Title;
                        }
                        else
                        {
                            imageToAdd = null;
                        }
                        searchedSubjectsImages.Add(imageToAdd);
                    }

                    ViewBag.SearchedImages = searchedSubjectsImages;
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
                if (positions.Count == 0)
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
            _routeService.PopRoutesOutOfIndexFromList(Request.Url.OriginalString, Routes);
            return RedirectToAction("Index", new { subjectId = id });
        }
        public ActionResult PreIndex(int subjectId)
        {
            var currentRoute = _subjectService.GetSubjectById(1);
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

﻿using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IslamicGuide.Data.ViewModels.Position;
using IslamicGuide.Data.ViewModels.Shared;
using IslamicGuide.Services.Utilities;

namespace IslamicGuide.App.Controllers
{
    public class PositionsController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly PositionService _positionService;
        private readonly RouteService _routeService;
        public PositionsController()
        {
            _subjectService = new SubjectService();
            _positionService = new PositionService();
            _routeService = new RouteService();
        }
        // GET: Positions
        public ActionResult Index(int id, int? page)
        {
            string path;
            if (Request.Url.OriginalString.Contains("page"))
            {
                path = Request.Url.OriginalString;
                path = path.Substring(0, path.IndexOf("?"));

            }
            else
            {
                path = Request.Url.OriginalString;
            }
            try
            {
                Routes = new List<Route>();
                var parentSubject = _subjectService.GetSubjectById(id);
                Routes = _routeService.AddAllPreviousRoutesOfRequest(id,Request.UrlReferrer.ToString(),"?");
                _routeService.RouteHandling(
                    path,
                    new Title()
                    {
                        ArabicName = parentSubject.Title,
                        EnglishName = parentSubject.Title_English
                    },
                    parentSubject.Title_English,
                    parentSubject.Title,
                    "Topic",
                    "Index",
                    id,
                    Routes);
                ViewBag.subjectTitle = (LangCode == "en") ? parentSubject.Title_English : parentSubject.Title;
            }
            catch (Exception e)
            {

                return View("Error");
            }
            Search(id, page);

            //Pagination Problem 
            ViewBag.PathForPaging = path+"?";
            ViewBag.PagingResult.Url = path + "?";

            //Pagination Problem 

            //// Routing And title Handling
            //var positionDetials = _positionService.GetPositionDetials(id, LangCode);
            ViewBag.HedayetAlAyat = _subjectService.GetHedayetAlAyat(id, LangCode);
            ViewBag.Routes = Routes;
            return View();
        }

        public void Search(int id, int? page)
        {
            int pageSize = 10;
            PositionPageVM pm = new PositionPageVM();
            var result = _positionService.AdjustingPositionData(new PageFilterModel()
            {
                LangCode = LangCode,
                PageSize = pageSize,
                Skip = ((page ?? 1) - 1) * pageSize
            }, id);
            //Logic
            var pagesCount = 1;
            if (result.RowsCount % pageSize == 0)
                pagesCount = (result.RowsCount / pageSize);
            else
                pagesCount = (result.RowsCount / pageSize) + 1;
            pm.DataList = result;
            pm.SubjectId = id;
            ViewBag.pm = pm;
            ViewBag.PagingResult = new PagingModel
            {
                CurrentPage = page ?? 1,

                PagesCount = pagesCount
            };
        }

        public ActionResult GetSpecificContent(int positionId , int bookId)
        {
            var positionDetials = _positionService.GetPositionDetials(positionId, LangCode);
            ViewBag.NextAya = positionDetials.NextAyaWords;
            ViewBag.PrevAya = positionDetials.PrevAyaWords;
            ViewBag.PositionId = positionId;
            var allBooks = _positionService.GetPositionContent(positionId, bookId,LangCode,true);

            var p = _positionService.GetPositionSpecificContent(positionId,LangCode,bookId);
            if (positionDetials != null)
            {
                ViewBag.BooksDDL = allBooks;

                if (p != null)
                {
                    positionDetials.BookContent = p;
                }
            }
            return View("GetPositionDetails",positionDetials);
        }

        public ActionResult GetPositionDetails(int id, int? tab, int? page)
        {
            
            var positionDetials = _positionService.GetPositionDetials(id, LangCode);
            ViewBag.NextAya = positionDetials.NextAyaWords;
            ViewBag.PrevAya = positionDetials.PrevAyaWords;
            ViewBag.PositionId = id;
            if (tab == null)
            {
                tab = 4;
                ViewBag.Tab = 4;
            }
            else
                ViewBag.Tab = tab;
            var p = _positionService.GetPositionContent(id,tab,LangCode,false);
            if (positionDetials != null && p != null && p.Count() != 0)
            {
                ViewBag.BooksDDL = p;
                positionDetials.BookContent = p;
                ViewBag.tabId = tab;
            }

            var subject = _positionService.GetSubjectTitleForPosition(id);
            var subjectTitle = new Title()
            {
                ArabicName = subject.Title,
                EnglishName = subject.Title_English
            };
            var path = Request.Url.Authority + "/topic/Index/";
            Routes = _routeService.AddAllPreviousRoutesOfRequest(subject.ID, path, "/");
            Routes.Add(new Route()
            {
                ActionName = "Index",
                Controller = "Topic",
                Id = subject.ID,
                Text = new Title()
                {
                    ArabicName = subject.Title,
                    EnglishName = subject.Title_English
                },
                Uri = Request.Url.OriginalString
            });
            //_routeService.RouteHandling(
            //    Request.Url.OriginalString,
            //    new Title()
            //    {
            //        ArabicName = subjectTitle.ArabicName,
            //        EnglishName = subjectTitle.EnglishName
            //    },
            //    positionDetials.SuraTitle_English,
            //    positionDetials.SuraTitle,
            //    "Positions",
            //    "Index",
            //    id,
            //    Routes);
            ViewBag.Routes = Routes;
            return View(positionDetials);
        }
    }
}

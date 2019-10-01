using IslamicGuide.Services.BussinessServices;
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
        private readonly PositionService _positionService ;
        private readonly RouteService _routeService;
        public PositionsController()
        {
            _subjectService = new SubjectService();
            _positionService = new PositionService();
            _routeService = new RouteService();
        }
        // GET: Positions
        public ActionResult Index(int id, int ? page)
        {
            Search(id, page);
            // Routing And title Handling
            var positionDetials = _positionService.GetPositionDetials(id, LangCode);

            var parentTitle = _subjectService.subjectTitle(id, LangCode);
            _routeService.RouteHandling(positionDetials.SuraTitle, "Positions", "Index", id, Routes);
            ViewBag.subjectTitle = parentTitle;
            return View();
        }

        public void Search(int id, int? page)
        {
            int pageSize = 6;
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
        public ActionResult GetById(int id)
        {
            
            var positionDetials = _positionService.GetPositionDetials(id,LangCode);
            _routeService.RouteHandling(positionDetials.SuraTitle,"Positions","GetById",id,Routes);
            return View(positionDetials);
        }

        public ActionResult GetPositionDetails(int id,int ? tab, int? page)
        {
           var p = _positionService.GetPositionContent(id,null);
           return View("Index");
        }
    }
}

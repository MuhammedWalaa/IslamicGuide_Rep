using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IslamicGuide.Services.Utilities;

namespace IslamicGuide.App.Controllers
{
    public class SubTopicPositionsController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly PositionService _positionService ;
        private readonly RouteService _routeService;
        public SubTopicPositionsController()
        {
            _subjectService = new SubjectService();
            _positionService = new PositionService();
            _routeService = new RouteService();
        }
        // GET: SubTopicPositions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
            
            var positionDetials = _positionService.GetPositionDetials(id);
            _routeService.RouteHandling(positionDetials.SuraTitle,"SubTopicPositions","GetById",id,Routes);
            return View(positionDetials);
        }
    }
}

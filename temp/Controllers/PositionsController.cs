using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
            
            var positionDetials = _positionService.GetPositionDetials(id,LangCode);
            _routeService.RouteHandling(positionDetials.SuraTitle,"Positions","GetById",id,Routes);
            return View(positionDetials);
        }
    }
}

using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IslamicGuide.Services.Utilities;

namespace IslamicGuide.App.Controllers
{
    public class SubTopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly PositionService _positionService;
        private readonly RouteService _routeService;
        public SubTopicController()
        {
            _subjectService = new SubjectService();
            _positionService = new PositionService(); 
            _routeService=new RouteService();
        }
        // GET: SubTopic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
           
            var positions = _positionService.GetSubjectAndSubSubjectPositionsById(id);
            string parentTitle = _subjectService.subjectTitle(id);

            _routeService.RouteHandling(parentTitle,"SubTopicController","GetById",id,Routes);
            ViewBag.subjectTitle = parentTitle;
            return View(positions);
        }

    }
}

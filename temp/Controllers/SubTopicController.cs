using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{
    public class SubTopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly PositionService _positionService;
        public SubTopicController()
        {
            _subjectService = new SubjectService();
            _positionService = new PositionService();
        }   
        // GET: SubTopic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
            var positions = _positionService.GetSubjectAndSubSubjectPositionsById(id);
            var parentTitle = _subjectService.subjectTitle(id);
            ViewBag.subjectTitle = parentTitle;
            return View(positions);
        }

    }
}

using IslamicGuide.Services.BussinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{
    public class SubTopicPositionsController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly PositionService _positionService ;
        public SubTopicPositionsController()
        {
            _subjectService = new SubjectService();
            _positionService = new PositionService();
        }
        // GET: SubTopicPositions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
            var positionDetials = _positionService.GetPositionDetials(id);
            return View(positionDetials);
        }
    }
}

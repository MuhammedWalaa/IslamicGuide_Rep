using IslamicGuide.Services.Services;
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
        public SubTopicController()
        {
            _subjectService = new SubjectService();
        }   
        // GET: SubTopic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
            var positions = _subjectService.GetSubSubjectPositionsById(id);
            var parentTitle = _subjectService.subjectTitle(id);
            ViewBag.subjectTitle = parentTitle;
            return View(positions);
        }

    }
}

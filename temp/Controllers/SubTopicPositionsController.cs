using IslamicGuide.Services.Services;
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
        public SubTopicPositionsController()
        {
            _subjectService = new SubjectService();
        }
        // GET: SubTopicPositions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
            var subSubjects = _subjectService.GetSubSubjectPositionsById(id);
            return View(subSubjects);
        }
    }
}

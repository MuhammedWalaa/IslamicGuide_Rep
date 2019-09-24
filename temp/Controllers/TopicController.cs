using IslamicGuide.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IslamicGuide.App.Controllers
{
    public class TopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        public TopicController()
        {
            _subjectService = new SubjectService();
        }
        // GET: Topic
        public ActionResult Index()
        {
            var subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }
    }
}

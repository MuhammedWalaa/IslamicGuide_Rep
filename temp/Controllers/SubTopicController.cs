using IslamicGuide.Services.Services;
using System.Web.Mvc;
using IslamicGuide.Services.Utilities;

namespace IslamicGuide.App.Controllers
{
    public class SubTopicController : BaseController
    {
        private readonly SubjectService _subjectService;
        private readonly RouteService _routeService;
        public SubTopicController()
        {
            _subjectService = new SubjectService();
            _routeService=new RouteService();
        }
        // GET: SubTopic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetById(int id)
        {
           
            var positions = _subjectService.GetSubSubjectPositionsById(id);
            string parentTitle = _subjectService.subjectTitle(id);

            _routeService.RouteHandling(parentTitle,"SubTopicController","GetById",id,Routes);
            ViewBag.subjectTitle = parentTitle;
            return View(positions);
        }

    }
}

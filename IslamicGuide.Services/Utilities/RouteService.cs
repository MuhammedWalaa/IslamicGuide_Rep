using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Shared;
using IslamicGuide.Services.BussinessServices;

namespace IslamicGuide.Services.Utilities
{
    public class RouteService
    {
        private readonly SubjectService _subjectService;
        public RouteService()
        {
            _subjectService = new SubjectService();
        }

        public void RouteHandling(string uri,Title parentTitle,string nameEnglish,string nameArabic, string controller, string actionName, int? id, List<Route> routes)
        {
            
            if (uri.Contains("&page"))
            {
                uri = uri.Remove(uri.LastIndexOf("&"), uri.Length - uri.LastIndexOf("&"));
            }
            if (uri.Contains("&tab"))
            {
                uri = uri.Remove(uri.LastIndexOf("&"), uri.Length - uri.LastIndexOf("&"));
            }

            if (routes.FirstOrDefault(r => r.Uri.ToLower()==uri.ToLower()) != null)
            {
                    PopRoutesOutOfIndex(uri,routes);
            }
            else
            {
                    AddRoute(uri,parentTitle,nameEnglish,nameArabic, controller, actionName, id, routes);
            }
            
        }

        public List<Route> AddAllPreviousRoutesOfRequest(int subjectId, string uri,string character)
        {
            var baseUri = uri.Remove(uri.LastIndexOf(character), uri.Length - uri.LastIndexOf(character));
            List<Route> routes = new List<Route>();
            var parents = _subjectService.GetListOfSubjectParents(subjectId);
            routes.Add(new Route()
            {
                ActionName = "Index",
                Controller = "Home",
                Text = new Title()
                {
                    ArabicName = "الرئيسية",
                    EnglishName = "Home"
                },
                Uri = baseUri
            });
            foreach (var parent in parents)
            {
             routes.Add(new Route()
             {
                 ActionName = "Index",
                 Controller = "Topic",
                 Text = new Title()
                 {
                     ArabicName = parent.Title,
                     EnglishName = parent.Title_English
                 },
                 Uri = baseUri+"/" +subjectId,
                 Id = parent.ID

             });   
            }

            return routes;
        }
        private void AddRoute(string uri,Title parentTitle,string nameEnglish,string nameArabic,string controller,string actionName,int ? id, List<Route> routes)
        {

            if (parentTitle!=null)
            {
                PopTillTheParent(parentTitle,routes);
            }
            //var routeTitles = routes.Select()
            if (id == null)
            {
                
                routes.Add(new Route() { Uri = uri, ActionName = actionName, Controller = controller, Text = new Title(){ArabicName = nameArabic, EnglishName = nameEnglish} });
                

            }
            else
            {
                routes.Add(new Route() {Uri = uri, ActionName = actionName, Controller = controller, Text = new Title() { ArabicName = nameArabic, EnglishName = nameEnglish }, Id=id });
            }

            
        }

        private int SearchForRoute(string uri, List<Route> routes)
        {
            int flag=0;
            foreach (var route in routes)
            {
                if (route.Uri.Equals(uri))
                {
                    return flag;
                }

                flag++;
            }

            return 5555;
        }

        public int SearchForParent(Title parentTitle, List<Route> routes)
        {
            int flag = 0;
            foreach (var route in routes)
            {
                if (route.Text.ArabicName.Equals(parentTitle.ArabicName))
                {
                    return flag;
                }

                flag++;
            }

            return 5555;
        }
        public void PopTillTheParent(Title parentTitle, List<Route> routes)
        {
            int size = routes.Count;
            int index = SearchForParent(parentTitle, routes);

            for (int i = index + 1; i < size; i++)
            {
                routes.RemoveAt(routes.Count - 1);
            }
        }
        public void PopRoutesOutOfIndex(string uri,List<Route> routes)
        {
            int size = routes.Count;
            int index = SearchForRoute(uri, routes);
            for (int i = index+1; i < size; i++)
            {
                routes.RemoveAt(routes.Count-1);
            }

            
        }
        public void PopRoutesOutOfIndexFromList(string uri,List<Route> routes)
        {
            int size = routes.Count;
            int index = SearchForRoute(uri, routes);

            for (int i = index ; i < size; i++)
            {
                routes.RemoveAt(routes.Count - 1);
            }


        }

    }
}

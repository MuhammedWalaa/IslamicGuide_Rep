using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IslamicGuide.Data.ViewModels.Shared;

namespace IslamicGuide.Services.Utilities
{
    public class RouteService
    {
        

        public void RouteHandling(string uri,Title parentTitle,string nameEnglish,string nameArabic, string controller, string actionName, int? id, List<Route> routes)
        {

            
                if (routes.FirstOrDefault(r => r.Uri.ToLower()==uri.ToLower()) != null)
                {
                    PopRoutesOutOfIndex(uri,routes);
                }
                else
                {
                    AddRoute(uri,parentTitle,nameEnglish,nameArabic, controller, actionName, id, routes);
                }
            
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

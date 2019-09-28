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
        public void RouteHandling(string name, string controller, string actionName, int? id, List<Route> routes)
        {
            if(routes.Count>0)
            if (name != null&&routes[1].Text!=null)
            {
                if (routes.LastOrDefault(r => r.Text.Equals(name)) != null)
                {
                    PopRoutesOutOfIndex(routes, name);
                }
                else
                {
                    AddRoute(name, controller, actionName, id, routes);
                }
            }
        }
        private void AddRoute(string name,string controller,string actionName,int ? id, List<Route> routes)
        {
            if (id == null)
            {
                routes.Add(new Route() { ActionName = actionName, Controller = controller, Text = name });
                

            }
            else
            {
                routes.Add(new Route() { ActionName = actionName, Controller = controller, Text = name,Id=id });
            }

            
        }

        private int SearchForRoute(string name, List<Route> routes)
        {
            int flag=0;
            foreach (var route in routes)
            {
                if (route.Text.Equals(name))
                {
                    return flag;
                }

                flag++;
            }

            return 5555;
        }

        private void PopRoutesOutOfIndex(List<Route> routes, string name)
        {
            int size = routes.Count;
            int index = SearchForRoute(name, routes);
            for (int i = index+1; i < size; i++)
            {
                routes.RemoveAt(routes.Count-1);
            }

            
        }


    }
}

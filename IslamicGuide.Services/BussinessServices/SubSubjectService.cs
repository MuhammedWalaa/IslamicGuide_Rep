using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Services.Services
{
    public class SubSubjectService
    {
        private readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        public SubSubjectService()
        {
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
        }
        
        public void GetSubSubjectById(int id)
        {
            //var subjects = _DbContext.sub.ToList();
            //foreach (var item in subjects)
            //{
            //    var x = item;
            //}
            //SubSubjects
            //return booksGrid;
        }
    }
}

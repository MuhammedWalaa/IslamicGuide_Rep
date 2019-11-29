using IslamicGuide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicGuide.Services.BussinessServices
{
    public class SubSubjectService
    {
        private readonly IslamicCenterEntities _DbContext;
        public SubSubjectService()
        {
            _DbContext = new IslamicCenterEntities();
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

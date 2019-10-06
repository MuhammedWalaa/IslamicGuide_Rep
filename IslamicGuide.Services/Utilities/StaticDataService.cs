using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;

namespace IslamicGuide.Services.Utilities
{
    public class StaticDataService
    {
        private readonly DB_A4DE6E_IslamicGuideEntities _db;

        public StaticDataService()
        {
            _db = new DB_A4DE6E_IslamicGuideEntities();
        }
        public List<PraysVM> GetPraysTime(string langCode)
        {
            List<PraysVM> praysVms = new List<PraysVM>();
            List<PraysTime> praysTime = new List<PraysTime>();
            try
            {
                praysTime = _db.PraysTimes.ToList();
                foreach (PraysTime p in praysTime)
                {
                    if (langCode.Equals("en"))
                    {
                        praysVms.Add(new PraysVM() { Name = p.Name_English, Time = p.Time });
                    }
                    else
                    {
                        praysVms.Add(new PraysVM() { Name = p.Name_Arabic, Time = p.Time });
                    }

                }
            }
            catch (System.Exception)
            {

                return null;
            }
            
            return praysVms;

        }

        public LayoutStaticDataVM GetLayoutStaticData(string langCode)
        {
            string location = "";


            List<StaticData> staticData = _db.StaticDatas.Where(d => d.Name == "Phone" || d.Name == "Email" || d.Name == "Location").ToList();
            if (langCode == "en")
            {
                location = staticData.FirstOrDefault(d => d.Name == "Location").Data_English;
            }
            else
            {
                location = staticData.FirstOrDefault(d => d.Name == "Location").Data_Arabic;
            }
            return new LayoutStaticDataVM()
            {
                Email = staticData.FirstOrDefault(d => d.Name == "Email").Data_Arabic,
                Phone = staticData.FirstOrDefault(d => d.Name == "Phone").Data_Arabic,
                Location = location
            };
        }
    }
}

using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;

namespace IslamicGuide.Services.Utilities
{
    public class StaticDataService
    {
        private readonly IslamicCenterEntities _db;

        public StaticDataService()
        {
            _db = new IslamicCenterEntities();
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
        public bool SaveContactUs(string name,string email,string comment,string phone="")
        {

            ContactUs obj = new ContactUs()
            {
                Name = name,
                Email = email,
                Comment = comment,
                Phone = phone,
            };
            try
            {
                _db.ContactUs1.Add(obj);
                _db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        public LayoutStaticDataVM GetLayoutStaticData(string langCode)
        {
            string location = "";
            string there = "";
            List<StaticData> staticData = _db.StaticDatas.Where(d => d.Name == "Phone" || d.Name == "Email" || d.Name == "Location"||d.Name== "There1").ToList();
            if (langCode == "en")
            {
                location = staticData.FirstOrDefault(d => d.Name == "Location").Data_English;
                there = staticData.FirstOrDefault(d => d.Name == "There1").Data_English;
            }
            else
            {
                location = staticData.FirstOrDefault(d => d.Name == "Location").Data_Arabic;
                there = staticData.FirstOrDefault(d => d.Name == "There1").Data_Arabic;
            }
            return new LayoutStaticDataVM()
            {

                Email = staticData.FirstOrDefault(d => d.Name == "Email").Data_Arabic,
                Phone = staticData.FirstOrDefault(d => d.Name == "Phone").Data_Arabic,
                Location = location,
                There = there,
            };
        }
    }
}

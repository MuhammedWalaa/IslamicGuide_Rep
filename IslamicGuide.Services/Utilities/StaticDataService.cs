using IslamicGuide.Data;
using IslamicGuide.Data.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using IslamicGuide.Data.ViewModels.Home;

namespace IslamicGuide.Services.Utilities
{
    public class StaticDataService
    {
        private readonly IslamicCenterEntities _db;

        public StaticDataService()
        {
            _db = new IslamicCenterEntities();
        }

        public BannerVM GetFirstBannerData(string langCode)
        {
            var bannerHeader = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("FirstBannerHeader"));
            var bannerFirstBody = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("FirstBannerFirstBody"));
            var bannerSecBody = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("FirstBannerSecBody"));
            var firstBanner = new BannerVM()
            {
                BannerFirstBody = bannerFirstBody!=null?(langCode=="en" && bannerFirstBody.Data_English !=null) ? bannerFirstBody.Data_English : bannerFirstBody.Data_Arabic:"",
                BannerHeader = bannerHeader!=null?(langCode == "en" && bannerHeader.Data_English != null) ? bannerHeader.Data_English : bannerHeader.Data_Arabic:"",
                BannerSecBody = bannerSecBody!=null?(langCode == "en" && bannerSecBody.Data_English != null) ? bannerSecBody.Data_English : bannerSecBody.Data_Arabic:""
            };
            return firstBanner;
        }
        public BannerVM GetSecBannerData(string langCode)
        {
            var bannerHeader = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("SecBannerHeader"));
            var bannerFirstBody = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("SecBannerFirstBody"));
            var bannerSecBody = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("SecBannerSecBody"));
            var secBanner = new BannerVM()
            {
                BannerFirstBody = bannerFirstBody != null ? (langCode == "en" && bannerFirstBody.Data_English != null) ? bannerFirstBody.Data_English : bannerFirstBody.Data_Arabic : "",
                BannerHeader = bannerHeader != null ? (langCode == "en" && bannerHeader.Data_English != null) ? bannerHeader.Data_English : bannerHeader.Data_Arabic : "",
                BannerSecBody = bannerSecBody != null ? (langCode == "en" && bannerSecBody.Data_English != null) ? bannerSecBody.Data_English : bannerSecBody.Data_Arabic : ""
            };
            return secBanner;
        }
        public BannerVM GetThirdBannerData(string langCode)
        {
            var bannerHeader = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("ThirdBannerHeader"));
            var bannerFirstBody = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("ThirdBannerFirstBody"));
            var bannerSecBody = _db.StaticDatas.FirstOrDefault(b => b.Name.Equals("ThirdBannerSecBody"));
            var thirdBanner = new BannerVM()
            {
                BannerFirstBody = bannerFirstBody != null ? (langCode == "en" && bannerFirstBody.Data_English != null) ? bannerFirstBody.Data_English : bannerFirstBody.Data_Arabic : "",
                BannerHeader = bannerHeader != null ? (langCode == "en" && bannerHeader.Data_English != null) ? bannerHeader.Data_English : bannerHeader.Data_Arabic : "",
                BannerSecBody = bannerSecBody != null ? (langCode == "en" && bannerSecBody.Data_English != null) ? bannerSecBody.Data_English : bannerSecBody.Data_Arabic : ""
            };
            return thirdBanner;
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

            ContactUsInfo obj = new ContactUsInfo()
            {
                Name = name,
                Email = email,
                Comment = comment,
                Phone = phone,
            };
            try
            {
                _db.ContactUsInfoes.Add(obj);
                _db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public string GetAboutPageDataSection(string langCode)
        {
            var data = _db.StaticDatas.FirstOrDefault(sD => sD.Name.Equals("About"));
            return data !=null ?(langCode == "en" && data.Data_English != null) ? data.Data_English : data.Data_Arabic:"";
        }
        public LayoutStaticDataVM GetLayoutStaticData(string langCode)
        {
            string location = "";
            string miniAboutUs = "";
            List<StaticData> staticData = new List<StaticData>();
            staticData = _db.StaticDatas.Where(d => d.Name == "Phone" || d.Name == "Email" || d.Name == "Location"||d.Name== "MiniAboutUs").ToList();
            if (langCode == "en")
            {
                location = staticData.FirstOrDefault(d => d.Name == "Location")?.Data_English;
                miniAboutUs = staticData.FirstOrDefault(d => d.Name == "MiniAboutUs")?.Data_English;
            }
            else
            {
                location = staticData.FirstOrDefault(d => d.Name == "Location")?.Data_Arabic;
                miniAboutUs = staticData.FirstOrDefault(d => d.Name == "MiniAboutUs")?.Data_Arabic;
            }
            return new LayoutStaticDataVM()
            {

                Email = staticData.FirstOrDefault(d => d.Name == "Email")?.Data_Arabic,
                Phone = staticData.FirstOrDefault(d => d.Name == "Phone")?.Data_Arabic,
                Location = location,
                There = miniAboutUs,
            };
        }
    }
}

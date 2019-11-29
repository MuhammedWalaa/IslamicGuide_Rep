using IslamicGuide.Data;
using IslamicGuide.Services.Utilities;
using System.Linq;

namespace SchedualedJobs
{
    public class Program
    {
        private static readonly HttpService _httpService;
        private static readonly IslamicCenterEntities _DbContext;
        static Program()
        {
            _DbContext = new IslamicCenterEntities();
            _httpService = new HttpService();

        }
        public static void Main(string[] args)
        {

            ResponseResult response = GetJsonObject();

            PraysTime fajr = _DbContext.PraysTimes.FirstOrDefault(p => p.Name_English.Equals("Fajr"));
            fajr.Time = response.data.timings.Fajr;
            PraysTime dhuhr = _DbContext.PraysTimes.FirstOrDefault(p => p.Name_English.Equals("Dhuhr"));
            dhuhr.Time = response.data.timings.Dhuhr;
            PraysTime asr = _DbContext.PraysTimes.FirstOrDefault(p => p.Name_English.Equals("Asar"));
            asr.Time = response.data.timings.Asr;
            PraysTime maghrib = _DbContext.PraysTimes.FirstOrDefault(p => p.Name_English.Equals("Maghrib"));
            maghrib.Time = response.data.timings.Maghrib;
            PraysTime isha = _DbContext.PraysTimes.FirstOrDefault(p => p.Name_English.Equals("Isha"));
            isha.Time = response.data.timings.Isha;

            _DbContext.SaveChanges();



        }


        public static ResponseResult GetJsonObject()
        {

            string url = "https://api.aladhan.com/timingsByAddress/25-09-2019?address=makkah,KSA&method=8";
            return HttpService.DownloadJsonData<ResponseResult>(url, null);
        }
    }
}

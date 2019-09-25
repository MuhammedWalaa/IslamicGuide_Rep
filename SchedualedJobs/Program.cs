using IslamicGuide.Data;
using IslamicGuide.Services.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace SchedualedJobs
{
    public class Program
    {
        private static readonly HttpService _httpService;
        private static readonly DB_A4DE6E_IslamicGuideEntities _DbContext;
        static Program()
        {
            _DbContext = new DB_A4DE6E_IslamicGuideEntities();
            _httpService = new HttpService();

        }
        public static void Main(string[] args)
        {

            Task<ResponseResult> response = GettingResponseResult();

            Pray fajr = _DbContext.Prays.FirstOrDefault(p => p.Name_English.Equals("Fajr"));
            fajr.Time = response.Result.data.timings.Fajr;
            Pray dhuhr = _DbContext.Prays.FirstOrDefault(p => p.Name_English.Equals("Dhuhr"));
            dhuhr.Time = response.Result.data.timings.Dhuhr;
            Pray asr = _DbContext.Prays.FirstOrDefault(p => p.Name_English.Equals("Asr"));
            asr.Time = response.Result.data.timings.Asr;
            Pray maghrib = _DbContext.Prays.FirstOrDefault(p => p.Name_English.Equals("Maghrib"));
            maghrib.Time = response.Result.data.timings.Maghrib;
            Pray isha = _DbContext.Prays.FirstOrDefault(p => p.Name_English.Equals("Isha"));
            isha.Time = response.Result.data.timings.Isha;

            _DbContext.SaveChanges();



        }

        public static async Task<ResponseResult> GettingResponseResult()
        {
            ResponseResult response = await
                _httpService.SendHttpGetRequest(
                    "https://api.aladhan.com/timingsByAddress/25-09-2019?address=makkah,KSA&method=8");
            return response;
        }
    }
}

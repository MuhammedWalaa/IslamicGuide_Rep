namespace IslamicGuide.Data
{
    public class ResponseResult
    {
        public bool IsSuccess { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Timing timings { get; set; }
        public Date date { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string latitude { get; set; }
        public string longitude { get; set; }

        public string timezone { get; set; }

        public string lalatitudeAdjustmentMethodtitude { get; set; }

        public string midnightMode { get; set; }

        public string school { get; set; }

        public Offset offset { get; set; }

        public Method method { get; set; }



    }

    public class Method
    {
        public string id { get; set; }
        public string name { get; set; }

    }


    public class Offset
    {
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Sunset { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Imsak { get; set; }
        public string Midnight { get; set; }
    }

    public class Date
    {
        public string readable { get; set; }
        public string timestamp { get; set; }
    }

    public class Timing
    {
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Sunset { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Imsak { get; set; }
        public string Midnight { get; set; }

    }
}


    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    
namespace WorldApi
{
    public partial class Sunrise
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("time")]
        public List<Time> Time { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }
    }

    public partial class Time
    {
        [JsonProperty("low_moon", NullValueHandling = NullValueHandling.Ignore)]
        public HighMoon LowMoon { get; set; }

        [JsonProperty("high_moon", NullValueHandling = NullValueHandling.Ignore)]
        public HighMoon HighMoon { get; set; }

        [JsonProperty("solarmidnight", NullValueHandling = NullValueHandling.Ignore)]
        public HighMoon Solarmidnight { get; set; }

        [JsonProperty("moonrise", NullValueHandling = NullValueHandling.Ignore)]
        public Moonrise Moonrise { get; set; }

        [JsonProperty("moonset", NullValueHandling = NullValueHandling.Ignore)]
        public Moonrise Moonset { get; set; }

        [JsonProperty("moonposition")]
        public Moonposition Moonposition { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("sunset", NullValueHandling = NullValueHandling.Ignore)]
        public Moonrise Sunset { get; set; }

        [JsonProperty("moonphase", NullValueHandling = NullValueHandling.Ignore)]
        public Moonphase Moonphase { get; set; }

        [JsonProperty("solarnoon", NullValueHandling = NullValueHandling.Ignore)]
        public HighMoon Solarnoon { get; set; }

        [JsonProperty("moonshadow", NullValueHandling = NullValueHandling.Ignore)]
        public HighMoon Moonshadow { get; set; }

        [JsonProperty("sunrise", NullValueHandling = NullValueHandling.Ignore)]
        public Moonrise Sunrise { get; set; }
    }

    public partial class HighMoon
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("elevation")]
        public string Elevation { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("azimuth", NullValueHandling = NullValueHandling.Ignore)]
        public string Azimuth { get; set; }
    }

    public partial class Moonphase
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Moonposition
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("phase")]
        public string Phase { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("azimuth")]
        public string Azimuth { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("elevation")]
        public string Elevation { get; set; }
    }

    public partial class Moonrise
    {
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("licenseurl")]
        public Uri Licenseurl { get; set; }
    }
}

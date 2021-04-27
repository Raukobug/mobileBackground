    
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
namespace WorldApi
{
    public partial class GoogleTimeZone
    {
        [JsonProperty("dstOffset")]
        public long DstOffset { get; set; }

        [JsonProperty("rawOffset")]
        public long RawOffset { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timeZoneId")]
        public string TimeZoneId { get; set; }

        [JsonProperty("timeZoneName")]
        public string TimeZoneName { get; set; }
    }
}
namespace Huta.Prop
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Prop
    {
        [JsonProperty("intro")]
        public List<BeforeAndAfterData> Intro { get; set; }

        [JsonProperty("daily")]
        public List<DailyMessage> Daily { get; set; }

        [JsonProperty("end")]
        public List<BeforeAndAfterData> End { get; set; }
    }

    public partial class DailyMessages
    {
        public List<DailyMessage> Messages { get; set; }
    }

    public partial class DailyMessage
    {
        [JsonProperty("Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("SubMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string SubMessage { get; set; }

        [JsonProperty("ImagePath", NullValueHandling = NullValueHandling.Ignore)]
        public string ImagePath { get; set; }
    }

    public partial class BeforeAndAfterData
    {
        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("SubMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string SubMessage { get; set; }
    }

    public partial class Prop
    {
        public static Prop FromJson(string json) => JsonConvert.DeserializeObject<Prop>(json, Huta.Prop.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Prop self) => JsonConvert.SerializeObject(self, Huta.Prop.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

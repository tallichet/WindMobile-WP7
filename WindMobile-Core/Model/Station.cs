using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Model
{
    [DataContract]
    public class Station
    {
        [DataMember(Name="_id")]
        public string ID { get; set; }

        [DataMember(Name="status")]
        public string StatusString { get; set; }

        [DataMember(Name="loc")]
        public Location Location { get; set; }

        [DataMember(Name="short")]
        public string ShortName { get; set; }

        [DataMember(Name="name")]
        public string DisplayName { get; set; }

        [DataMember(Name="tags")]
        public string[] Tags { get; set; }

        [DataMember(Name="prov")]
        public string Provider { get; set; }

        [DataMember(Name="cat")]
        public string Category { get; set; }

        [DataMember(Name="seen")]
        public long LastSeen { get; set; }

        [DataMember(Name = "alt")]
        public int Altitude { get; set; }

        [DataMember(Name="timezone")]
        public string TimeZoneString { get; set; }

        [DataMember(Name="last")]
        public StationData Last { get; set; }

        [DataMember(Name="url")]
        public string Url { get; set; }

        [DataMember(Name="desc")]
        public string Description { get; set; }
    }

    [DataContract]
    public class Location
    {
        [DataMember(Name="lat")]
        public double Latitude { get; set; }

        [DataMember(Name="lon")]
        public double Longitude { get; set; }
    }

    [DataContract]
    public class StationData
    {
        [DataMember(Name="_id")]
        public string ID { get; set; }

        [DataMember(Name="w-dir")]
        public int WindDirection { get; set; }

        [DataMember(Name="temp")]
        public double Temperature { get; set; }

        [DataMember(Name = "w-min")]
        public int WindMin { get; set; }
        [DataMember(Name = "w-max")]
        public int WindMax { get; set; }

        [DataMember(Name = "w-avg")]
        public int WindAverage { get; set; }

        [DataMember(Name = "w-inst")]
        public int WindInstant { get; set; }


        [DataMember(Name="hum")]
        public double Humidity { get; set; }
    }
}

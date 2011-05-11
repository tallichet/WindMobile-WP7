using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Ch.Epyx.WindMobile.WP7.Model.Json
{
    [DataContract(Name="StationInfo", Namespace="ch.epyx.windmobile")]
    [KnownType(typeof(MaintenanceStatus))]
    [JsonObject]
    public class StationInfo : IStationInfo
    {

        [JsonProperty(PropertyName = "@maintenanceStatus")]
        [DataMember]
        public MaintenanceStatus MaintenanceStatus
        {
            get; set;
        }

        [JsonProperty(PropertyName = "@altitude")]
        [DataMember]
        public int Altitude
        {
            get; set;
        }

        [JsonProperty(PropertyName = "@wgs84Longitude")]
        [DataMember]
        public double Wgs84Longitude
        {
            get; set;
        }

        [JsonProperty(PropertyName = "@wgs84Latitude")]
        [DataMember]
        public double Wgs84Latitude
        {
            get; set;
        }

        [JsonProperty(PropertyName = "@dataValidity")]
        [DataMember]
        public int DataValidity
        {
            get; set;
        }

        [JsonProperty(PropertyName="@name")]
        [DataMember]
        public string Name
        {
            get; set;
        }

        [JsonProperty(PropertyName = "@shortName")]
        [DataMember]
        public string ShortName
        {
            get; set;
        }

        [JsonProperty(PropertyName = "@id")]
        [DataMember]
        public string Id
        {
            get; set;
        }


        public int CompareTo(IStationInfo other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}

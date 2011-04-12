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
using System.Xml.Linq;

namespace Ch.Epix.WindMobile.WP7.Model.Xml
{
    public class LastUpdate : ILastUpdate
    {
        private XElement _lastUpdate;

        internal LastUpdate(XElement lastUpdate)
        {
            this._lastUpdate = lastUpdate;
        }

        public DateTime Date
        {
            get { return DateTime.Parse(_lastUpdate.Value); }
        }
    }
}

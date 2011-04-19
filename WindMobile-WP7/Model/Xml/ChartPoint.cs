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
    public class ChartPoint : IChartPoint
    {
        public ChartPoint(XElement element)
        {
            Date = new DateTime(long.Parse(element.Element("date").Value));
            Value = double.Parse(element.Element("value").Value);
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public double Value
        {
            get;
            private set;
        }
    }
}

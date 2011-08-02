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
using System.Globalization;

namespace Ch.Epyx.WindMobile.WP7.Model.Xml
{
    public class ChartPoint : IChartPoint
    {
        static DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);

        public ChartPoint(XElement element)
        {
            Date = dt.AddMilliseconds(long.Parse(element.Element("date").Value));
            Value = double.Parse(element.Element("value").Value, CultureInfo.InvariantCulture.NumberFormat);
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public long DateAsLong
        {
            get { return Date.Ticks; }
        }

        public double Value
        {
            get;
            private set;
        }
    }
}

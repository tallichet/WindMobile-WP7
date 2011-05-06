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
using System.Collections.Generic;

namespace Ch.Epix.WindMobile.WP7.Model.Xml
{
    public class ChartSerie : XmlBase, IChartSerie
    {
        private List<IChartPoint> values;

        public ChartSerie(XElement element)
            : base(element)
        {

        }

        public string Name
        {
            get { return element.Attribute("name").Value ; }
        }

        public List<IChartPoint> Values
        {
            get 
            {
                if (values == null)
                {
                    values = new List<IChartPoint>();
                    foreach (var point in element.Elements("points"))
                    {
                        values.Add(new ChartPoint(point));
                    }
                }
                return values;
            }
        }

        public double MaxValue
        {
            get 
            {
                double maxValue = double.MinValue;
                foreach (var val in Values)
                {
                    if (val.Value > maxValue) maxValue = val.Value;
                }
                return maxValue;
            }
        }
    }
}

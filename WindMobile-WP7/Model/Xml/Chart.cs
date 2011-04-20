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
    public class Chart : XmlBase, IChart
    {
        IChartSerie average;
        IChartSerie max;

        public Chart(XElement element)
            : base(element)
        {
            foreach (var xmlSerie in element.Elements("serie"))
            {
                var serie = new ChartSerie(xmlSerie);
                if (serie.Name == "windAverage") average = serie;
                else if (serie.Name == "windMax") max = serie;                
            }
        }

        public IChartSerie WindAverage
        {
            get { return average; }
        }

        public IChartSerie WindMax
        {
            get { return max; }
        }
    }
}

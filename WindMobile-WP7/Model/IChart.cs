using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch.Epix.WindMobile.WP7.Model
{
    public interface IChart
    {
        IChartSerie WindAverage { get; }
        IChartSerie WindMax { get; }
    }
}

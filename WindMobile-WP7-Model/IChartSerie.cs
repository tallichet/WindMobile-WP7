using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch.Epyx.WindMobile.WP7.Model
{
    public interface IChartSerie
    {
        string Name { get; }
        List<IChartPoint> Values { get; }
        double MaxValue { get; }
    }
}

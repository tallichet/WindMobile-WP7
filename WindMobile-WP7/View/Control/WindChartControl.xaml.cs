using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Ch.Epyx.WindMobile.WP7.Model;

namespace Ch.Epyx.WindMobile.WP7.View
{
    public partial class WindChartControl : UserControl
    {
        public WindChartControl()
        {
            InitializeComponent();
        }

        public List<IChartPoint> ChartPoints
        {
            get { return (List<IChartPoint>)GetValue(ChartPointsProperty); }
            set { SetValue(ChartPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChartPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartPointsProperty =
            DependencyProperty.Register("ChartPoints", typeof(List<IChartPoint>), typeof(WindChartControl), new PropertyMetadata(OnChartPointsChanged));
        

        public static void OnChartPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WindChartControl charControl = d as WindChartControl;
            GeometryGroup myGeometryGroup = new GeometryGroup();
            var values = e.NewValue as List<IChartPoint>;
            
            double radius = 0;
            double lineRadius = charControl.DrawCanvas.ActualWidth / 2;
            double radiusStep = lineRadius / values.Count;
            // The center
            double lastX = (double)(charControl.DrawCanvas.ActualWidth / 2.0);
            double lastY = (double)(charControl.DrawCanvas.ActualHeight / 2.0);

            foreach (var value in values)
            {
                radius += radiusStep;

                double pointOffsetX = (charControl.DrawCanvas.ActualWidth - 2.0 * radius) / 2.0;
                double pointOffsetY = (charControl.DrawCanvas.ActualHeight - 2.0 * radius) / 2.0;

                double circleX = Math.Cos(GetAngleInRadian(value)) * radius;
                double circleY = Math.Sin(GetAngleInRadian(value)) * radius;

                float x = (float)(pointOffsetX + radius - circleX);
                float y = (float)(pointOffsetY + radius - circleY);

                myGeometryGroup.Children.Add(new LineGeometry() { StartPoint = new Point(lastX, lastY), EndPoint = new Point(x, y) });

                lastX = x;
                lastY = y;
            }
            
            (d as WindChartControl).LinePath.Data = myGeometryGroup;
        }

        private static double GetAngleInRadian(IChartPoint value)
        {
            return Math.PI * (value.Value + 90.0) / 180.0;
        }
    }
}

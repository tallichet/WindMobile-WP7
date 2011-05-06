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

namespace Ch.Epyx.WindMobile.WP7.Model.Xml
{
    public class XmlBase
    {
        protected XElement element;


        public XmlBase(XElement element)
        {
            this.element = element;
        }
    }
}

using System;
using System.Xml.Linq;
using System.Globalization;

namespace Ch.Epyx.WindMobile.WP7.Model.Xml
{
    public class SocialMessage : XmlBase, ISocialMessage
    {
        public SocialMessage(XElement e)
            : base(e)
        {
        }

        public DateTime Date
        {
            get { return DateTime.Parse(element.Element("date").Value, CultureInfo.InvariantCulture.DateTimeFormat); }
        }

        public string Pseudo
        {
            get { return element.Element("pseudo").Value; }
        }

        public string Text
        {
            get { return element.Element("text").Value; }
        }
    }
}

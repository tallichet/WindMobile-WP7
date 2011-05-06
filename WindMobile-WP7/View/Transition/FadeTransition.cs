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
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Windows.Resources;
using System.IO;
using System.Windows.Markup;

namespace Ch.Epyx.WindMobile.WP7.View.Transition
{
    public class FadeTransition : TransitionElement
    {
        private static Dictionary<string, string> _storyboardXamlCache;



        public FadeMode FadeMode
        {
            get { return (FadeMode)GetValue(FadeModeProperty); }
            set { SetValue(FadeModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FadeMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FadeModeProperty =
            DependencyProperty.Register("FadeMode", typeof(FadeMode), typeof(FadeTransition), new PropertyMetadata(FadeMode.FadeIn));



        public override ITransition GetTransition(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            Storyboard storyboard = GetStoryboard(FadeMode.ToString());
            Storyboard.SetTarget(storyboard, element);
            element.Projection = new PlaneProjection { CenterOfRotationX = 0.5, CenterOfRotationY = 0.5 };
            return new Microsoft.Phone.Controls.Transition(element, storyboard);
        }

        /// <summary>
        /// Creates a
        /// <see cref="T:System.Windows.Media.Storyboard"/>
        /// for a particular transition family and transition mode.
        /// </summary>
        /// <param name="name">The transition family and transition mode.</param>
        /// <returns>The <see cref="T:System.Windows.Media.Storyboard"/>.</returns>
        private static Storyboard GetStoryboard(string name)
        {
            if (_storyboardXamlCache == null)
            {
                _storyboardXamlCache = new Dictionary<string, string>();
            }
            string xaml = null;
            if (_storyboardXamlCache.ContainsKey(name))
            {
                xaml = _storyboardXamlCache[name];
            }
            else
            {
                string path = "/Ch.Epix.WindMobile.WP7;component/View/Transition/Storyboard/" + name + ".xaml";
                Uri uri = new Uri(path, UriKind.Relative);
                StreamResourceInfo streamResourceInfo = Application.GetResourceStream(uri);
                using (StreamReader streamReader = new StreamReader(streamResourceInfo.Stream))
                {
                    xaml = streamReader.ReadToEnd();
                    _storyboardXamlCache[name] = xaml;
                }
            }
            return XamlReader.Load(xaml) as Storyboard;
        }
    }

    public enum FadeMode
    {
        FadeIn,
        FadeOut
    }
}

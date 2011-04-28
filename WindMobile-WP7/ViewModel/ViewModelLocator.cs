/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:WindMobile_WP7.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:WindMobile_WP7.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

using System.Collections.Generic;
using Ch.Epix.WindMobile.WP7.Model;
using Ch.Epix.WindMobile.WP7.Service;
using System;
namespace Ch.Epix.WindMobile.WP7.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:WindMobile_WP7.ViewModel"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:WindMobile_WP7.ViewModel"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public class ViewModelLocator
    {
        private static MainViewModel main;
        private static DataViewModel data;
        private static Dictionary<IStationInfo, StationInfoViewModel> infos;
        private static Dictionary<IStationInfo, ChartViewModel> charts;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (main == null)
                {
                    main = new MainViewModel();
                }
                return main;
            }
        }

        public static DataViewModel DataStatic
        {
            get
            {
                if (data == null)
                {
                    data = new DataViewModel();
                }
                return data;
            }
        }

        public static ExceptionViewModel ExceptionStatic
        {
            get;
            set;
        }

        public static Dictionary<IStationInfo, StationInfoViewModel> InfoViewModelsStatic
        {
            get
            {
                if (infos == null)
                {
                    infos = new Dictionary<IStationInfo, StationInfoViewModel>(ServiceCentral.ListService.LastResult.Count);
                    foreach (var info in ServiceCentral.ListService.LastResult)
                    {
                        infos.Add(info, new StationInfoViewModel(info));           
                    }                    
                }
                return infos;
            }
        }

        public static StationInfoViewModel CurrentInfoViewModelStatic
        {
            get
            {
                if (ApplicationRunData.CurrentStationStatic == null) throw new Exception("No station selected");
                return InfoViewModelsStatic[ApplicationRunData.CurrentStationStatic];
            }
        }


        public static Dictionary<IStationInfo, ChartViewModel> ChartViewModelsStatic
        {
            get
            {
                if (charts == null)
                {
                    charts = new Dictionary<IStationInfo, ChartViewModel>(ServiceCentral.ListService.LastResult.Count);
                    foreach (var info in ServiceCentral.ListService.LastResult)
                    {
                        charts.Add(info, new ChartViewModel(info));
                    }
                }
                return charts;
            }
        }

        public static ChartViewModel CurrentChartViewModelStatic
        {
            get
            {
                if (ApplicationRunData.CurrentStationStatic == null) throw new Exception("No station selected");
                return ChartViewModelsStatic[ApplicationRunData.CurrentStationStatic];
            }
        }
        
        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DataViewModel Data
        {
            get
            {
                return DataStatic;
            }
        }

        /// <summary>
        /// Gets the StationInfo property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public StationInfoViewModel CurrentStationInfoViewModel
        {
            get
            {
                return CurrentInfoViewModelStatic;
            }
        }

        /// <summary>
        /// Gets the StationInfo property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ChartViewModel CurrentChartViewModel
        {
            get
            {
                return CurrentChartViewModelStatic;
            }
        }

        /// <summary>
        /// Gets the StationInfo property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ExceptionViewModel Exception
        {
            get
            {
                return ExceptionStatic;   
            }
            set
            {
                ExceptionStatic = value;
            }
        }
    }
}
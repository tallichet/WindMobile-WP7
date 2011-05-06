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
using Ch.Epix.WindMobile.WP7.Model;
using System.Collections.Generic;
using Ch.Epix.WindMobile.WP7.Service.Job;
using Ch.Epix.WindMobile.WP7.Service.TypedServices;

namespace Ch.Epix.WindMobile.WP7.Service
{
    public class ServiceCentral
    {
        private static ListStationInfoService listService;
        private static AutoCreateIndexer<IStationInfo, StationDataService> dataServices;
        private static AutoCreateIndexer<IStationInfo, StationChartService> chartServices;
        private static System.Device.Location.GeoCoordinateWatcher coordinateWatcher;

        public static ListStationInfoService ListService
        {
            get
            {
                if (listService == null)
                {
                    listService = new ListStationInfoService();
                }
                return listService;
            }
        }

        public static AutoCreateIndexer<IStationInfo, StationDataService> DataServices
        {
            get
            {
                if (dataServices == null)
                {
                    dataServices = new AutoCreateIndexer<IStationInfo, StationDataService>(
                        (info) => new StationDataService(() => new GetStationDataJob(info))
                    );
                }
                return dataServices;
            }
        }

        public static AutoCreateIndexer<IStationInfo, StationChartService> ChartServices
        {
            get
            {
                if (chartServices == null)
                {
                    chartServices = new AutoCreateIndexer<IStationInfo, StationChartService>(
                        (info) =>
                            new StationChartService(() => new GetStationChartJob(info))
                    );
                }
                return chartServices;
            }
        }

        public static BaseService<object, IStationData> CurrentDataService
        {
            get
            {
                if (ApplicationRunData.CurrentStationStatic != null)
                {
                    return DataServices[ApplicationRunData.CurrentStationStatic];
                }
                return null;
            }
        }

        public static StationChartService CurrentChartService
        {
            get
            {
                if (ApplicationRunData.CurrentStationStatic != null)
                {
                    return ChartServices[ApplicationRunData.CurrentStationStatic];
                }
                return null;
            }
        }

        public static System.Device.Location.GeoCoordinateWatcher GeoCoordinateWatcher
        {
            get
            {
                if (coordinateWatcher == null)
                {
                    coordinateWatcher = new System.Device.Location.GeoCoordinateWatcher();
                    if (coordinateWatcher.Status != System.Device.Location.GeoPositionStatus.Disabled)
                    {
                        coordinateWatcher.Start();
                    }
                }
                return coordinateWatcher;
            }
        }
    }

    public class AutoCreateIndexer<K, T>
    {
        public delegate T CreateAction(K key);
        private Dictionary<K, T> baseDictionary;
        private CreateAction Create;

        public AutoCreateIndexer(CreateAction action) 
        {
            baseDictionary = new Dictionary<K, T>();
            Create = action;
        }

        public T this[K key] 
        {
            get
            {
                if (baseDictionary.ContainsKey(key) == false)
                {
                    baseDictionary[key] = Create(key);
                }
                return baseDictionary[key];
            }
            set
            {
                baseDictionary[key] = value;
            }
        }
    }
}

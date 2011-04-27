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

namespace Ch.Epix.WindMobile.WP7.Service
{
    public class ServiceCentral
    {
        private static BaseService<object, List<IStationInfo>> listService;
        private static AutoCreateIndexer<IStationInfo, BaseService<string, IStationData>> dataServices;
        private static AutoCreateIndexer<IStationInfo, BaseService<string, IChart>> chartServices;

        public static BaseService<object, List<IStationInfo>> ListService
        {
            get
            {
                if (listService == null)
                {
                    listService = new BaseService<object, List<IStationInfo>>(
                        () => { return new ListStationInfoJob(); }
                        );
                }
                return listService;
            }
        }

        public static AutoCreateIndexer<IStationInfo, BaseService<string, IStationData>> DataServices
        {
            get
            {
                if (dataServices == null)
                {
                    dataServices = new AutoCreateIndexer<IStationInfo, BaseService<string, IStationData>>(
                        (info) => 
                        {
                            return new BaseService<string, IStationData>(
                                () =>
                                {
                                    return new GetStationDataJob();
                                });
                        });
                }
                return dataServices;
            }
        }

        public static AutoCreateIndexer<IStationInfo, BaseService<string, IChart>> ChartServices
        {
            get
            {
                if (chartServices == null)
                {
                    chartServices = new AutoCreateIndexer<IStationInfo, BaseService<string, IChart>>(
                        (info) =>
                        {
                            return new BaseService<string, IChart>(
                                () =>
                                {
                                    return new GetStationChartJob(info);
                                });
                        });
                }
                return chartServices;
            }
        }

        public static BaseService<string, IStationData> CurrentDataService
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

        public static BaseService<string, IChart> CurrentChartService
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

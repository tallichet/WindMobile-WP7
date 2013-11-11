using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core
{
    public static class Helper
    {
        public static async Task<string> ReadAsString(this System.IO.Stream stream)
        {
            using (stream)
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static string Serialize<T>(T obj)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            var byteArray = ms.ToArray();
            string retVal = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
            }
            return obj;
        }

        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        //public static void ReplaceItemById<T>(this IList<T> list, T newItem) where T : Model.IItemWithId
        //{
        //    if (list != null && newItem != null)
        //    {
        //        var itemToReplace = list.FirstOrDefault(i => i.Id == newItem.Id);
        //        if (itemToReplace != null)
        //        {
        //            var idx = list.IndexOf(itemToReplace);
        //            list.Remove(itemToReplace);
        //            list.Insert(idx, newItem);
        //        }
        //        else
        //        {
        //            list.Add(newItem);
        //        }
        //    }
        //}
    }
}

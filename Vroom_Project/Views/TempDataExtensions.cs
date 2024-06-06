﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Vroom_Project.Views.Test
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key)
        {
            tempData.TryGetValue(key, out var obj);
            return obj == null ? default(T) : JsonConvert.DeserializeObject<T>((string)obj);
        }
    }
}

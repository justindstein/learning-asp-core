﻿using Newtonsoft.Json;

namespace learning_asp_core.Utils
{
    public static class ObjectExtension
    {
        public static string Dump(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
/// This file is auto generated at 2021-03-19T09:46:29

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebExtension.Net.ExtensionTypes
{
    /// MultiType Definition
    /// <summary></summary>
    public class Date
    {
        private readonly string valuestring;
        public Date(string valuestring)
        {
            this.valuestring = valuestring;
        }
        
        private readonly int valueint;
        public Date(int valueint)
        {
            this.valueint = valueint;
        }
        
        private readonly object valueobject;
        public Date(object valueobject)
        {
            this.valueobject = valueobject;
        }
        
    }
}


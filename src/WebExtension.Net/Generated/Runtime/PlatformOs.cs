/// This file is auto generated at 2021-03-19T09:46:29

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebExtension.Net.Runtime
{
    /// Enum Definition
    /// <summary>The operating system the browser is running on.</summary>
    [JsonConverter(typeof(EnumStringConverter<PlatformOs>))]
    public enum PlatformOs
    {
        [EnumValue("mac")]
        Mac,
        [EnumValue("win")]
        Win,
        [EnumValue("android")]
        Android,
        [EnumValue("cros")]
        Cros,
        [EnumValue("linux")]
        Linux,
        [EnumValue("openbsd")]
        Openbsd,
    }
}


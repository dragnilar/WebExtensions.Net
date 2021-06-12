using System.Text.Json.Serialization;

namespace WebExtensions.Net.Runtime
{
    // Type Class
    /// <summary>An object containing information about the current browser.</summary>
    public partial class BrowserInfo : BaseObject
    {
        private string _buildID;
        private string _name;
        private string _vendor;
        private string _version;

        /// <summary>The browser's build ID/date, for example '20160101'.</summary>
        [JsonPropertyName("buildID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string BuildID
        {
            get
            {
                InitializeProperty("buildID", _buildID);
                return _buildID;
            }
            set
            {
                _buildID = value;
            }
        }

        /// <summary>The name of the browser, for example 'Firefox'.</summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name
        {
            get
            {
                InitializeProperty("name", _name);
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>The name of the browser vendor, for example 'Mozilla'.</summary>
        [JsonPropertyName("vendor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Vendor
        {
            get
            {
                InitializeProperty("vendor", _vendor);
                return _vendor;
            }
            set
            {
                _vendor = value;
            }
        }

        /// <summary>The browser's version, for example '42.0.0' or '0.8.1pre'.</summary>
        [JsonPropertyName("version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Version
        {
            get
            {
                InitializeProperty("version", _version);
                return _version;
            }
            set
            {
                _version = value;
            }
        }
    }
}

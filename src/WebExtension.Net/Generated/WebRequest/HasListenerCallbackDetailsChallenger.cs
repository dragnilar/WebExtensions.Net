using System.Text.Json.Serialization;

namespace WebExtension.Net.WebRequest
{
    // Type Class
    /// <summary>The server requesting authentication.</summary>
    public class HasListenerCallbackDetailsChallenger : BaseObject
    {
        private string _host;
        private int _port;

        /// <summary></summary>
        [JsonPropertyName("host")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Host
        {
            get
            {
                InitializeProperty("host", _host);
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        /// <summary></summary>
        [JsonPropertyName("port")]
        public int Port
        {
            get
            {
                InitializeProperty("port", _port);
                return _port;
            }
            set
            {
                _port = value;
            }
        }
    }
}

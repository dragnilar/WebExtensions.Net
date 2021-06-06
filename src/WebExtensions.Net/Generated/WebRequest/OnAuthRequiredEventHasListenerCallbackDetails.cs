using System.Text.Json.Serialization;

namespace WebExtensions.Net.WebRequest
{
    // Type Class
    /// <summary></summary>
    public class OnAuthRequiredEventHasListenerCallbackDetails : BaseObject
    {
        private HasListenerCallbackDetailsChallenger _challenger;
        private string _cookieStoreId;
        private string _documentUrl;
        private int _frameId;
        private bool? _incognito;
        private bool _isProxy;
        private string _method;
        private string _originUrl;
        private int _parentFrameId;
        private string _realm;
        private string _requestId;
        private HttpHeaders _responseHeaders;
        private string _scheme;
        private int _statusCode;
        private string _statusLine;
        private int _tabId;
        private bool _thirdParty;
        private double _timeStamp;
        private ResourceType _type;
        private string _url;
        private UrlClassification _urlClassification;

        /// <summary>The server requesting authentication.</summary>
        [JsonPropertyName("challenger")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HasListenerCallbackDetailsChallenger Challenger
        {
            get
            {
                InitializeProperty("challenger", _challenger);
                return _challenger;
            }
            set
            {
                _challenger = value;
            }
        }

        /// <summary>The cookie store ID of the contextual identity.</summary>
        [JsonPropertyName("cookieStoreId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string CookieStoreId
        {
            get
            {
                InitializeProperty("cookieStoreId", _cookieStoreId);
                return _cookieStoreId;
            }
            set
            {
                _cookieStoreId = value;
            }
        }

        /// <summary>URL of the page into which the requested resource will be loaded.</summary>
        [JsonPropertyName("documentUrl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string DocumentUrl
        {
            get
            {
                InitializeProperty("documentUrl", _documentUrl);
                return _documentUrl;
            }
            set
            {
                _documentUrl = value;
            }
        }

        /// <summary>The value 0 indicates that the request happens in the main frame; a positive value indicates the ID of a subframe in which the request happens. If the document of a (sub-)frame is loaded (<c>type</c> is <c>main_frame</c> or <c>sub_frame</c>), <c>frameId</c> indicates the ID of this frame, not the ID of the outer frame. Frame IDs are unique within a tab.</summary>
        [JsonPropertyName("frameId")]
        public int FrameId
        {
            get
            {
                InitializeProperty("frameId", _frameId);
                return _frameId;
            }
            set
            {
                _frameId = value;
            }
        }

        /// <summary>True for private browsing requests.</summary>
        [JsonPropertyName("incognito")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Incognito
        {
            get
            {
                InitializeProperty("incognito", _incognito);
                return _incognito;
            }
            set
            {
                _incognito = value;
            }
        }

        /// <summary>True for Proxy-Authenticate, false for WWW-Authenticate.</summary>
        [JsonPropertyName("isProxy")]
        public bool IsProxy
        {
            get
            {
                InitializeProperty("isProxy", _isProxy);
                return _isProxy;
            }
            set
            {
                _isProxy = value;
            }
        }

        /// <summary>Standard HTTP method.</summary>
        [JsonPropertyName("method")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Method
        {
            get
            {
                InitializeProperty("method", _method);
                return _method;
            }
            set
            {
                _method = value;
            }
        }

        /// <summary>URL of the resource that triggered this request.</summary>
        [JsonPropertyName("originUrl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string OriginUrl
        {
            get
            {
                InitializeProperty("originUrl", _originUrl);
                return _originUrl;
            }
            set
            {
                _originUrl = value;
            }
        }

        /// <summary>ID of frame that wraps the frame which sent the request. Set to -1 if no parent frame exists.</summary>
        [JsonPropertyName("parentFrameId")]
        public int ParentFrameId
        {
            get
            {
                InitializeProperty("parentFrameId", _parentFrameId);
                return _parentFrameId;
            }
            set
            {
                _parentFrameId = value;
            }
        }

        /// <summary>The authentication realm provided by the server, if there is one.</summary>
        [JsonPropertyName("realm")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Realm
        {
            get
            {
                InitializeProperty("realm", _realm);
                return _realm;
            }
            set
            {
                _realm = value;
            }
        }

        /// <summary>The ID of the request. Request IDs are unique within a browser session. As a result, they could be used to relate different events of the same request.</summary>
        [JsonPropertyName("requestId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RequestId
        {
            get
            {
                InitializeProperty("requestId", _requestId);
                return _requestId;
            }
            set
            {
                _requestId = value;
            }
        }

        /// <summary>The HTTP response headers that were received along with this response.</summary>
        [JsonPropertyName("responseHeaders")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HttpHeaders ResponseHeaders
        {
            get
            {
                InitializeProperty("responseHeaders", _responseHeaders);
                return _responseHeaders;
            }
            set
            {
                _responseHeaders = value;
            }
        }

        /// <summary>The authentication scheme, e.g. Basic or Digest.</summary>
        [JsonPropertyName("scheme")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Scheme
        {
            get
            {
                InitializeProperty("scheme", _scheme);
                return _scheme;
            }
            set
            {
                _scheme = value;
            }
        }

        /// <summary>Standard HTTP status code returned by the server.</summary>
        [JsonPropertyName("statusCode")]
        public int StatusCode
        {
            get
            {
                InitializeProperty("statusCode", _statusCode);
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        /// <summary>HTTP status line of the response or the 'HTTP/0.9 200 OK' string for HTTP/0.9 responses (i.e., responses that lack a status line) or an empty string if there are no headers.</summary>
        [JsonPropertyName("statusLine")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string StatusLine
        {
            get
            {
                InitializeProperty("statusLine", _statusLine);
                return _statusLine;
            }
            set
            {
                _statusLine = value;
            }
        }

        /// <summary>The ID of the tab in which the request takes place. Set to -1 if the request isn't related to a tab.</summary>
        [JsonPropertyName("tabId")]
        public int TabId
        {
            get
            {
                InitializeProperty("tabId", _tabId);
                return _tabId;
            }
            set
            {
                _tabId = value;
            }
        }

        /// <summary>Indicates if this request and its content window hierarchy is third party.</summary>
        [JsonPropertyName("thirdParty")]
        public bool ThirdParty
        {
            get
            {
                InitializeProperty("thirdParty", _thirdParty);
                return _thirdParty;
            }
            set
            {
                _thirdParty = value;
            }
        }

        /// <summary>The time when this signal is triggered, in milliseconds since the epoch.</summary>
        [JsonPropertyName("timeStamp")]
        public double TimeStamp
        {
            get
            {
                InitializeProperty("timeStamp", _timeStamp);
                return _timeStamp;
            }
            set
            {
                _timeStamp = value;
            }
        }

        /// <summary>How the requested resource will be used.</summary>
        [JsonPropertyName("type")]
        public ResourceType Type
        {
            get
            {
                InitializeProperty("type", _type);
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary></summary>
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Url
        {
            get
            {
                InitializeProperty("url", _url);
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        /// <summary>Tracking classification if the request has been classified.</summary>
        [JsonPropertyName("urlClassification")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UrlClassification UrlClassification
        {
            get
            {
                InitializeProperty("urlClassification", _urlClassification);
                return _urlClassification;
            }
            set
            {
                _urlClassification = value;
            }
        }
    }
}

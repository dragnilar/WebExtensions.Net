using System.Text.Json.Serialization;

namespace WebExtension.Net.Tabs
{
    // Type Class
    /// <summary></summary>
    public class AddListenerCallbackAttachInfo : BaseObject
    {
        private int _newPosition;
        private int _newWindowId;

        /// <summary></summary>
        [JsonPropertyName("newPosition")]
        public int NewPosition
        {
            get
            {
                InitializeProperty("newPosition", _newPosition);
                return _newPosition;
            }
            set
            {
                _newPosition = value;
            }
        }

        /// <summary></summary>
        [JsonPropertyName("newWindowId")]
        public int NewWindowId
        {
            get
            {
                InitializeProperty("newWindowId", _newWindowId);
                return _newWindowId;
            }
            set
            {
                _newWindowId = value;
            }
        }
    }
}

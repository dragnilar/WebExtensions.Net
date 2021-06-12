using System.Text.Json.Serialization;

namespace WebExtensions.Net.Bookmarks
{
    // Type Class
    /// <summary></summary>
    public partial class HasListenerCallbackMoveInfo : BaseObject
    {
        private int _index;
        private int _oldIndex;
        private string _oldParentId;
        private string _parentId;

        /// <summary></summary>
        [JsonPropertyName("index")]
        public int Index
        {
            get
            {
                InitializeProperty("index", _index);
                return _index;
            }
            set
            {
                _index = value;
            }
        }

        /// <summary></summary>
        [JsonPropertyName("oldIndex")]
        public int OldIndex
        {
            get
            {
                InitializeProperty("oldIndex", _oldIndex);
                return _oldIndex;
            }
            set
            {
                _oldIndex = value;
            }
        }

        /// <summary></summary>
        [JsonPropertyName("oldParentId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string OldParentId
        {
            get
            {
                InitializeProperty("oldParentId", _oldParentId);
                return _oldParentId;
            }
            set
            {
                _oldParentId = value;
            }
        }

        /// <summary></summary>
        [JsonPropertyName("parentId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ParentId
        {
            get
            {
                InitializeProperty("parentId", _parentId);
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }
    }
}

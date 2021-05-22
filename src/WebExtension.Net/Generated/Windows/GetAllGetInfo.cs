using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebExtension.Net.Windows
{
    // Type Class
    /// <summary>Specifies properties used to filter the $(ref:windows.Window) returned and to determine whether they should contain a list of the $(ref:tabs.Tab) objects.</summary>
    public class GetAllGetInfo : BaseObject
    {
        private IEnumerable<WindowType> _windowTypes;

        /// <summary>If set, the $(ref:windows.Window) returned will be filtered based on its type. If unset the default filter is set to <c>['app', 'normal', 'panel', 'popup']</c>, with <c>'app'</c> and <c>'panel'</c> window types limited to the extension's own windows.</summary>
        [JsonPropertyName("windowTypes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<WindowType> WindowTypes
        {
            get
            {
                InitializeProperty("windowTypes", _windowTypes);
                return _windowTypes;
            }
            set
            {
                _windowTypes = value;
            }
        }
    }
}

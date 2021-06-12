using System.Threading.Tasks;

namespace WebExtensions.Net.PageAction
{
    /// <inheritdoc />
    public partial class PageActionApi : BaseApi, IPageActionApi
    {
        private OnClickedEvent _onClicked;

        /// <summary>Creates a new instance of <see cref="PageActionApi" />.</summary>
        /// <param name="webExtensionsJSRuntime">Web Extension JS Runtime</param>
        public PageActionApi(IWebExtensionsJSRuntime webExtensionsJSRuntime) : base(webExtensionsJSRuntime, "pageAction")
        {
        }

        /// <inheritdoc />
        public OnClickedEvent OnClicked
        {
            get
            {
                if (_onClicked is null)
                {
                    _onClicked = new OnClickedEvent();
                    InitializeProperty("onClicked", _onClicked);
                }
                return _onClicked;
            }
        }

        /// <inheritdoc />
        public virtual ValueTask<string> GetPopup(GetPopupDetails details)
        {
            return InvokeAsync<string>("getPopup", details);
        }

        /// <inheritdoc />
        public virtual ValueTask<string> GetTitle(GetTitleDetails details)
        {
            return InvokeAsync<string>("getTitle", details);
        }

        /// <inheritdoc />
        public virtual ValueTask Hide(int tabId)
        {
            return InvokeVoidAsync("hide", tabId);
        }

        /// <inheritdoc />
        public virtual ValueTask IsShown(IsShownDetails details)
        {
            return InvokeVoidAsync("isShown", details);
        }

        /// <inheritdoc />
        public virtual ValueTask OpenPopup()
        {
            return InvokeVoidAsync("openPopup");
        }

        /// <inheritdoc />
        public virtual ValueTask SetIcon(SetIconDetails details)
        {
            return InvokeVoidAsync("setIcon", details);
        }

        /// <inheritdoc />
        public virtual ValueTask SetPopup(SetPopupDetails details)
        {
            return InvokeVoidAsync("setPopup", details);
        }

        /// <inheritdoc />
        public virtual ValueTask SetTitle(SetTitleDetails details)
        {
            return InvokeVoidAsync("setTitle", details);
        }

        /// <inheritdoc />
        public virtual ValueTask Show(int tabId)
        {
            return InvokeVoidAsync("show", tabId);
        }
    }
}

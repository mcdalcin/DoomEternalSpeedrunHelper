using System;

using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace DoomEternalSpeedrunHelper2.Contracts.Services
{
    public interface IWebViewService
    {
        event EventHandler<CoreWebView2WebErrorStatus> NavigationCompleted;

        bool CanGoBack { get; }

        bool CanGoForward { get; }

        void Initialize(WebView2 webView);

        void UnregisterEvents();

        void GoBack();

        void GoForward();

        void Reload();
    }
}

using System;

using DoomEternalSpeedrunHelper2.Contracts.Services;

using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace DoomEternalSpeedrunHelper2.Services
{
    public class WebViewService : IWebViewService
    {
        private WebView2 _webView;

        public bool CanGoBack
            => _webView.CanGoBack;

        public bool CanGoForward
            => _webView.CanGoForward;

        public event EventHandler<CoreWebView2WebErrorStatus> NavigationCompleted;

        public WebViewService()
        {
        }

        public void Initialize(WebView2 webView)
        {
            _webView = webView;
            _webView.NavigationCompleted += OnWebViewNavigationCompleted;
        }

        public void UnregisterEvents()
        {
            _webView.NavigationCompleted -= OnWebViewNavigationCompleted;
        }

        private void OnWebViewNavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            NavigationCompleted?.Invoke(this, args.WebErrorStatus);
        }

        public void GoBack()
            => _webView.GoBack();

        public void GoForward()
            => _webView.GoForward();

        public void Reload()
            => _webView.Reload();
    }
}

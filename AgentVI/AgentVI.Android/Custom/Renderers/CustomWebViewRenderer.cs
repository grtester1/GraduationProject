using AgentVI.Custom.Renderers;
using AgentVI.Droid.Custom.Renderers;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace AgentVI.Droid.Custom.Renderers
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context i_Context) : base(i_Context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.DisplayZoomControls = false;
                Control.Settings.LoadWithOverviewMode = true;
                Control.Settings.UseWideViewPort = true;
            }
        }

        /*
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control.SetWebViewClient(new ExtendedWebViewClient(Element as CustomWebView));
        }

        private class ExtendedWebViewClient : Android.Webkit.WebViewClient
        {
            CustomWebView m_CustomWebView;

            public ExtendedWebViewClient(CustomWebView i_CustomWebView)
            {
                m_CustomWebView = i_CustomWebView;
            }

            async public override void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                if (m_CustomWebView != null)
                {
                    int i = 10;
                    while (view.ContentHeight == 0 && i-- > 0)
                    {
                        await System.Threading.Tasks.Task.Delay(100);
                    }
                    m_CustomWebView.HeightRequest = view.ContentHeight;
                    (m_CustomWebView.Parent.Parent as ViewCell).ForceUpdateSize();
                }
                base.OnPageFinished(view, url);
            }
        }
        */
    }
}
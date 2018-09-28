using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Utils
{
    public static class Settings
    {
        public static Thickness iOSPlatformPadding { get; } = new Thickness(5);
        public static Thickness ListViewMargins { get; } = new Thickness(0,5,0,5);
        public static string LoadingAnimationPath { get; } = "resource://AgentVI.Sources.LoadingAnimation.gif";
        public static string BackgroundPicturePath { get; } = "resource://AgentVI.Sources.background.png";
        public static string LogoPicturePath { get; } = "resource://AgentVI.Sources.innovi_logo.png";
    }
}

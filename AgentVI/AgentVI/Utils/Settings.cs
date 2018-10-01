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

        public static Color FooterColor { get; } = Color.AliceBlue;
        public static Color FooterSeparatorColor { get; } = Color.FromHex("#DCDCDC");
        public static Color SensorEventFontColor { get; } = Color.Navy;

        public static string LoadingAnimationPath { get; } = "resource://AgentVI.Sources.LoadingAnimation.gif";
        public static string EmptyCollectionSVGPath { get; } = "resource://AgentVI.Sources.EmptyPageFiller.svg";
        public static string BackButtonSVGPath { get; } = "resource://AgentVI.Sources.Icons.backButton.svg";
        public static string DateTimeFormat { get; } = "dd/MM/yyyy hh:mm tt";

        public static ImageSource BackgroundPicturePath { get; } = ImageSource.FromResource("AgentVI.Sources.background.png");
        public static ImageSource LogoPicturePath { get; } = ImageSource.FromResource("AgentVI.Sources.innovi_logo.png");

        public static double SensorEventNameFontSize { get; } = 20;
        public static double SensorEventDetailsFontSize { get; } = 16;
    }
}

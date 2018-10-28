using System;
using System.Collections.Generic;
using System.Text;
using AgentVI.Interfaces;
using Plugin.DeviceOrientation.Abstractions;
using Xamarin.Forms;

namespace AgentVI.Utils
{
    public class UpdatedContentEventArgs : EventArgs
    {
        public ContentPage UpdatedContent { get; private set; }
        public EContentUpdateType ContentUpdateType { get; private set; }
        public IBindableVM UpdatedVM { get; private set; }
        public enum EContentUpdateType { Push, PushAsync, Pop, PopAsync, Buffering, None};

        public UpdatedContentEventArgs(EContentUpdateType i_ContentUpdateType = EContentUpdateType.None, ContentPage i_UpdatedContent = null, IBindableVM i_UpdatedVM = null)
        {
            UpdatedContent = i_UpdatedContent;
            UpdatedVM = i_UpdatedVM;
            ContentUpdateType = i_ContentUpdateType;
            if (i_UpdatedContent == null && isValidForPushRequest(i_ContentUpdateType))
            {
                throw new ArgumentException("Incorrect use of UpdatedContentEventArgs");
            }
        }

        private bool isValidForPushRequest(EContentUpdateType i_ContentUpdateType)
        {
            return i_ContentUpdateType == EContentUpdateType.Push || i_ContentUpdateType == EContentUpdateType.PushAsync;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Utils
{
    public class UpdatedContentEventArgs : EventArgs
    {
        public ContentPage UpdatedContent { get; private set; }
        public bool IsStackPopRequested { get; private set; } = false;

        public UpdatedContentEventArgs(ContentPage i_UpdatedContent = null, bool i_IsStackPopRequested = false)
        {
            UpdatedContent = i_UpdatedContent;
            IsStackPopRequested = i_IsStackPopRequested;
        }
    }
}

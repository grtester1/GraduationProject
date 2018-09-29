using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Utils
{
    public class UpdatedContentEventArgs : EventArgs
    {
        public ContentPage UpdatedContent { get; private set; }

        public UpdatedContentEventArgs(ContentPage i_UpdatedContent)
        {
            UpdatedContent = i_UpdatedContent;
        }
    }
}

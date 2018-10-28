using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Interfaces
{
    public interface IBindable
    {
        IBindableVM BindableViewModel { get; }
        ContentPage ContentPage { get; }
    }
}

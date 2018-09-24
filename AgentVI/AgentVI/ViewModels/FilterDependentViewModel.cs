using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AgentVI.ViewModels
{
    public abstract class FilterDependentViewModel<T>
    {
        public ObservableCollection<T> ObservableCollection { get; set; }

        public abstract void OnFilterStateUpdated(object source, EventArgs e);
    }
}

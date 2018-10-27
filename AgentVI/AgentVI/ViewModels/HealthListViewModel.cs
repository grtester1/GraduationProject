#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.ComponentModel;
using System.Collections.Generic;

using Xamarin.Forms;
using AgentVI.Models;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentVI.Services;



namespace AgentVI.ViewModels
{
    public class HealthListViewModel : FilterDependentViewModel<HealthModel>
    {
        public override void OnFilterStateUpdated(object source, EventArgs e)
        {
            base.OnFilterStateUpdated(source, e);
            PopulateCollection();
        }

        public override void PopulateCollection()
        {
            base.PopulateCollection();
            enumerableCollection = ServiceManager.Instance.FilterService.
                                                 FilteredHealth.Select(health => HealthModel.FactoryMethod(health));
            FetchCollection();
        }
    }
}
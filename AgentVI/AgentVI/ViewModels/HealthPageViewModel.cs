using System;
using Xamarin.Forms;
using AgentVI.Models;

namespace AgentVI.ViewModels
{
    public class HealthPageViewMode : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }

        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((HealthModel)item).HealthDescription != "Resolution too low" ? ValidTemplate : InvalidTemplate;
        }
    }
}
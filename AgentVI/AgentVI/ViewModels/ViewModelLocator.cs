using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.ViewModels
{
    public class ViewModelLocator
    {
        public FilterViewModel FilterViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FilterViewModel>();
            }
        }
    }
}
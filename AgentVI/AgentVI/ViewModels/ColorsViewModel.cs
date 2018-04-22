using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.ViewModels
{
    public class ColorsViewModel
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public static IList<ColorsViewModel> All { get; set; }

        static ColorsViewModel()
        {
            All = new ObservableCollection<ColorsViewModel>
            {
                new ColorsViewModel{Name="red", Color=Color.Red},
                new ColorsViewModel{Name="green", Color=Color.Green},
                new ColorsViewModel{Name="yellow", Color=Color.Yellow}
            };
        }
    }
}

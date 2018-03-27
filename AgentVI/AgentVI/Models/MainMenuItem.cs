using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgentVI.Models
{
    public class MainMenuItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _m_Title;
        public string m_Title { get { return _m_Title; }  set { OnPropertyChanged(); _m_Title = value; }}
        private string _m_Icon;
        public string m_Icon { get { return _m_Icon; } set { OnPropertyChanged(); _m_Icon = value; }}
        private Page _m_Page;
        public Page m_Page { get { return _m_Page; } set { OnPropertyChanged(); _m_Page = value; }}

        public MainMenuItem()
        {
            m_Title = "Empty";
            m_Icon = "Empty";
            m_Page = null;
        }

        public MainMenuItem(string i_Title, string i_Icon, Page i_Page)
        {
            m_Title = i_Title;
            m_Icon = i_Icon;
            m_Page = i_Page;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //PropertyChangedEventHandler 
            var handler = PropertyChanged;
            if(handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

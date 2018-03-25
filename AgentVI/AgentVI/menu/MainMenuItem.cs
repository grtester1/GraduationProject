using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.menu
{
    public class MainMenuItem
    {
        public string m_Title { get; private set; }
        public string m_Icon { get; private set; }
        public Page m_Page { get; private set; }


        public MainMenuItem(string i_Title, string i_Icon, Page i_Page)
        {
            m_Title = i_Title;
            m_Icon = i_Icon;
            m_Page = i_Page;
        }
    }
}

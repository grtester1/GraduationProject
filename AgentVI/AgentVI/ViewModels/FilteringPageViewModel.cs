using InnoviApiProxy;
//<debugAmi>using DummyProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.ViewModels
{
    public class FilteringPageViewModel
    {
        public int FilterID { get; private set; }
        public List<Folder> FoldersList { get; private set; }

        public FilteringPageViewModel()
        {

        }

        public FilteringPageViewModel(List<Folder> i_folderList, int i_filterID) : this()
        {
            FoldersList = i_folderList;
            FilterID = i_filterID;
        }
    }
}

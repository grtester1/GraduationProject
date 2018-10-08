#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.ViewModels
{
    public class FilteringPageViewModel
    {
        public int FilterID { get; private set; }
        public IEnumerator FoldersList { get; private set; }

        private FilteringPageViewModel()
        {

        }

        public FilteringPageViewModel(IEnumerator i_folderList, int i_filterID) : this()
        {
            FoldersList = i_folderList;
            FilterID = i_filterID;
        }
    }
}

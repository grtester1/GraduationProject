using System;
using System.Collections.Generic;
using System.Text;
using InnoviApiProxy;

namespace AgentVI.Services
{
    public class FilterService
    {
        private List<Folder> AccountFolders_Depth0 { get; set; }
        private List<Folder> AccountFolders_Depth1 { get; set; }
        private List<Folder> AccountFolders_NextDepth { get; set; }

        public FilterService()
        {
            AccountFolders_Depth0 = null;
        }

        public List<Folder> getAccountFolders(User i_user)
        {
            if(AccountFolders_Depth0==null)
            {
                AccountFolders_Depth0 = i_user.GetDefaultAccountFolders();
                //foreach(Folder currentDepth0Folder in AccountFolders_Depth0)
                //{

                //}
            }
            return AccountFolders_Depth0;
        }
    }
}

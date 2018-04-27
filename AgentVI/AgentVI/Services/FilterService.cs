using System;
using System.Collections.Generic;
using System.Text;
using InnoviApiProxy;

namespace AgentVI.Services
{
    public class FilterService
    {
        private List<Folder> AccountFolders { get; set; }

        public FilterService()
        {
            AccountFolders = null;
        }

        public List<Folder> getAccountFolders(User i_user)
        {
            if(AccountFolders==null)
            {
                AccountFolders = i_user.GetDefaultAccountFolders();
            }
            return AccountFolders;
        }
    }
}

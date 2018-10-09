#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

namespace AgentVI.Services
{
    public interface IFilterService : IServiceModule
    {
        void                                SelectRootLevel();
        void                                SelectFolder(Folder i_FolderSelected);
        void                                SwitchAccount(Account i_SelectedAccount);
        event EventHandler                  FilterStateUpdated;
        Account                             CurrentAccount { get; }
        List<Account>                       UserAccounts { get; }
        IEnumerator                         FilteredSensorCollection { get; }
        IEnumerator                         CurrentLevel { get; }
        IEnumerator                         FilteredEvents { get; }
        bool                                IsAtRootLevel { get; }
        bool                                IsAtLeafFolder { get; }
        bool                                HasNextLevel { get; }
        List<Folder>                        CurrentPath { get; }
        Dictionary<Folder, IEnumerator>     NextLevel { get; }
    }
}

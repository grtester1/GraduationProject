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
        void                                SelectFolderAndTriggerFetchUpdate(Folder i_FolderSelected);
        void                                SwitchAccount(Account i_SelectedAccount);
        event EventHandler                  FilterStateUpdated;
        Account                             CurrentAccount { get; }
        List<Account>                       UserAccounts { get; }
        IEnumerable<Sensor>                 FilteredSensorCollection { get; }
        IEnumerable<Folder>                 CurrentLevel { get; }
        IEnumerable<SensorEvent>            FilteredEvents { get; }
        IEnumerable<Sensor.Health>          FilteredHealth { get; }
        bool                                IsAtRootLevel { get; }
        bool                                IsAtLeafFolder { get; }
        bool                                HasNextLevel { get; }
        List<Folder>                        CurrentPath { get; }
    }
}

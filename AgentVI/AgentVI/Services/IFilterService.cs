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
    public interface IFilterService
    {
        List<Folder>                        GetAllFoldersBeneath(List<Folder> i_folders);
        void                                InitCollections(InnoviObjectCollection<Folder> i_FolderCollection, InnoviObjectCollection<Sensor> i_SensorCollection);
        void                                FetchSelectedFolder();
        List<Folder>                        SelectFolder(Folder i_selectedFolder);
        bool                                IsEmptyFolder(Folder i_SelectedFolder);
        List<Folder>                        GetAccountFolders(User i_user);
        [Obsolete("Method GetFilteredSensorCollection is deprecated. Use instead GetFilteredSensorsEnumerator()")]
        List<Sensor>                        GetFilteredSensorCollection();
        List<String>                        GetSelectedFoldersHirearchy();
        string                              GetLeafFolder();
        IEnumerator                         GetFilteredSensorsEnumerator();
        IEnumerator                         GetFilteredEventsEnumerator();
        event EventHandler                  FilterStateUpdated;
        bool IsAtRootLevel                  { get; }
    }
}

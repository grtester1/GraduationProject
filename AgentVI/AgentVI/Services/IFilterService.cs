#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Services
{
    public interface IFilterService
    {
        List<Folder>        GetAllFoldersBeneath(List<Folder> i_folders);
        void                InitCollections(InnoviObjectCollection<Folder> i_FolderCollection, InnoviObjectCollection<Sensor> i_SensorCollection);
        List<Sensor>        GetFilteredSensorCollection();
        List<Folder>        SelectFolder(Folder i_selectedFolder);
        bool                IsEmptyFolder(Folder i_SelectedFolder);
        List<Folder>        GetAccountFolders(User i_user);
        List<String>        GetSelectedFoldersHirearchy();
    }
}

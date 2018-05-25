using InnoviApiProxy;
//using DummyProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Services
{
    public interface IFilterService
    {
        List<Folder> getAllFoldersBeneath(List<Folder> i_folders);

        List<Folder> selectFolder(Folder i_selectedFolder);

        List<Folder> getAccountFolders(User i_user);

        List<String> getSelectedFoldersHirearchy();
    }
}

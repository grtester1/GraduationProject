using System;
using System.Collections.Generic;
using System.Text;
using InnoviApiProxy;
using Xamarin.Forms;

namespace AgentVI.Services
{
    public class FilterService : IFilterService
    {
        private List<Folder> AccountFolders_Depth0 { get; set; }
        private List<List<Folder>> FilteringLevelsCache { get; set; }
        private List<String> SelectedFoldersNames { get; set; }

        public FilterService()
        {
            AccountFolders_Depth0 = null;
            FilteringLevelsCache = new List<List<Folder>>();
            SelectedFoldersNames = new List<string>();
        }
        
        public List<String> getSelectedFoldersHirearchy()
        {
            return SelectedFoldersNames;
        }

        /// <summary>
        /// <para>Provides a list of all folders underneath the current list of folders.</para>
        /// <exception cref="Exception">Throws exception when the tree of folders underneath given list isn't balanced.</exception>
        /// </summary>
        public List<Folder> getAllFoldersBeneath(List<Folder> i_folders)
        {
            List<Folder> result = null;
            String errorCode = "Given list of folders doesn't exist! # getAllFoldersBeneath";

            if (i_folders == null) throw new Exception(errorCode);

            foreach(Folder currentFolder in i_folders)
            {
                if (currentFolder == null) { throw new Exception(errorCode); }
                if (result == null)
                {
                    result = currentFolder.Folders;
                    if (result == null) { throw new Exception(errorCode); }
                }
                else
                {
                    List<Folder> appendedList = currentFolder.Folders;
                    if(appendedList == null) { throw new Exception(errorCode); }
                    result.AddRange(appendedList);
                }
            }

            return result;
        }

        public List<Folder> selectFolder(Folder i_selectedFolder)
        {
            if (i_selectedFolder == null)
            {
                throw new Exception("Selected item isn't a folder.");
            }
            if(i_selectedFolder.Depth!=FilteringLevelsCache.Count-1)
            {
                FilteringLevelsCache.RemoveRange(i_selectedFolder.Depth + 1, FilteringLevelsCache.Count - 1);
                SelectedFoldersNames.RemoveRange(i_selectedFolder.Depth + 1, SelectedFoldersNames.Count - 1);
            }
            if(i_selectedFolder!=null)
            {
                if (i_selectedFolder.Folders != null)
                {
                    FilteringLevelsCache.Add(i_selectedFolder.Folders);
                }
                else
                {
                    SelectedFoldersNames.Add(i_selectedFolder.Name);
                }
            }
            return FilteringLevelsCache[FilteringLevelsCache.Count-1];
        }

        public List<Folder> getAccountFolders(User i_user)
        {
            if(AccountFolders_Depth0 == null)
            {
                AccountFolders_Depth0 = i_user.GetDefaultAccountFolders();
                if (AccountFolders_Depth0 == null) { throw new Exception("No folders for current user."); }
                FilteringLevelsCache.Add(AccountFolders_Depth0);
            }
            return AccountFolders_Depth0;
        }
    }
}
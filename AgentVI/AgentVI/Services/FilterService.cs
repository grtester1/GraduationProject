#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Services
{
    public partial class ServiceManager
    {
        private class FilterServiceS : IFilterService
        {
            //TODO: merge AccountFolders_Depth0 & levelOneFolderCollection to 1 variable

            private List<Folder> AccountFolders_Depth0 { get; set; }
            private List<List<Folder>> FilteringLevelsCache { get; set; }
            private List<String> SelectedFoldersNames { get; set; }
            private List<Sensor> FilteredSensorsCollection { get; set; }
            private bool isFilterUpdated = false;
            public event EventHandler FilterStateUpdated;

            public FilterServiceS()
            {
                AccountFolders_Depth0 = null;
                FilteringLevelsCache = new List<List<Folder>>();
                SelectedFoldersNames = new List<String>();
            }

            public List<String> GetSelectedFoldersHirearchy()
            {
                return SelectedFoldersNames;
            }

            public void InitCollections(InnoviObjectCollection<Folder> i_FolderCollection,
                                        InnoviObjectCollection<Sensor> i_SensorCollection)
            {
                if (i_FolderCollection != null)
                    AccountFolders_Depth0 = i_FolderCollection.ToList();
                if (i_SensorCollection != null)
                    FilteredSensorsCollection = i_SensorCollection.ToList();
                isFilterUpdated = false;
            }

            protected virtual void OnFilterStateUpdated()
            {
                FilterStateUpdated?.Invoke(this, EventArgs.Empty);
            }

            public void SaveFilteredSensorCollection()
            {
                List<Sensor> res = null;
                List<Folder> bufListOfFolders = null;

                if (FilteringLevelsCache.Count == 0 || SelectedFoldersNames.Count == 0)
                {
                    res = FilteredSensorsCollection;
                }
                else
                {
                    if (isFilterUpdated)                                                                        //Filter is set
                    {
                        res = FilteredSensorsCollection;
                    }
                    else
                    {
                        if (FilteringLevelsCache[FilteringLevelsCache.Count - 1] == null)                       //Filter is set till the last level
                        {
                            bufListOfFolders = FilteringLevelsCache[FilteringLevelsCache.Count - 2];
                            Folder foundFolder = null;
                            foreach (Folder f in bufListOfFolders)
                            {
                                if (f.Name == SelectedFoldersNames[SelectedFoldersNames.Count - 1])
                                {
                                    foundFolder = f;
                                    break;
                                }
                            }
                            FilteredSensorsCollection = res = foundFolder.Sensors.ToList();
                        }
                        else                                                                                    //Filter is set but not till the last level (case "Filter1->Filter2" out of Filter1,Filter2,Filter3 is not considered!)
                        {
                            FilteredSensorsCollection = null;
                            foreach (Folder f in FilteringLevelsCache[FilteringLevelsCache.Count - 1])
                            {
                                if (FilteredSensorsCollection == null)
                                    FilteredSensorsCollection = f.Sensors.ToList();
                                else
                                {
                                    FilteredSensorsCollection.AddRange(f.Sensors.ToList());
                                }
                            }
                            res = FilteredSensorsCollection;
                        }
                        isFilterUpdated = true;
                    }
                }

                OnFilterStateUpdated();
            }

            public List<Sensor> GetFilteredSensorCollection()
            {
                return FilteredSensorsCollection;
            }

            /// <summary>
            /// <para>Provides a list of all folders underneath the current list of folders.</para>
            /// <exception cref="Exception">Throws exception when the tree of folders underneath given list isn't balanced.</exception>
            /// </summary>
            public List<Folder> GetAllFoldersBeneath(List<Folder> i_folders)
            {
                List<Folder> result = null;
                String errorCode = "Given list of folders doesn't exist! # getAllFoldersBeneath";

                if (i_folders == null) throw new Exception(errorCode);

                foreach (Folder currentFolder in i_folders)
                {
                    if (currentFolder == null) { throw new Exception(errorCode); }
                    if (result == null)
                    {
                        result = currentFolder.Folders.ToList();
                        if (result == null) { throw new Exception(errorCode); }
                    }
                    else
                    {
                        List<Folder> appendedList = currentFolder.Folders.ToList();
                        if (appendedList == null) { throw new Exception(errorCode); }
                        result.AddRange(appendedList);
                    }
                }

                return result;
            }

            public List<Folder> SelectFolder(Folder i_selectedFolder)
            {
                bool isLastFiltrationLevelSelection = false;
                if (i_selectedFolder == null)
                {
                    throw new Exception("Selected item isn't a folder.");
                }
                if (i_selectedFolder.Depth == FilteringLevelsCache.Count - 2)
                {
                    SelectedFoldersNames.RemoveRange(i_selectedFolder.Depth, SelectedFoldersNames.Count - 1);
                    isLastFiltrationLevelSelection = true;
                }
                else if (i_selectedFolder.Depth < FilteringLevelsCache.Count - 1)
                {
                    FilteringLevelsCache.RemoveRange(i_selectedFolder.Depth + 1, FilteringLevelsCache.Count - 1);
                    SelectedFoldersNames.RemoveRange(i_selectedFolder.Depth, SelectedFoldersNames.Count);
                }
                if (i_selectedFolder != null)
                {
                    if (i_selectedFolder.Folders != null && !isLastFiltrationLevelSelection)
                    {
                        FilteringLevelsCache.Add(i_selectedFolder.Folders.ToList());
                    }
                    SelectedFoldersNames.Add(i_selectedFolder.Name);

                }
                isFilterUpdated = false;
                return FilteringLevelsCache[FilteringLevelsCache.Count - 1];
            }

            public List<Folder> GetAccountFolders(User i_user)
            {
                if (AccountFolders_Depth0 == null)
                {
                    AccountFolders_Depth0 = i_user.GetDefaultAccountFolders().ToList();
                    if (AccountFolders_Depth0 == null) { throw new Exception("No folders for current user."); }
                    FilteringLevelsCache.Add(AccountFolders_Depth0);
                }
                return AccountFolders_Depth0;
            }

            public bool IsEmptyFolder(Folder i_SelectedFolder)
            {
                return i_SelectedFolder.Folders.IsEmpty();
            }
        }
    }
}
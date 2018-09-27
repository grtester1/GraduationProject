#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using AgentVI.Utils;
using System.Collections;

namespace AgentVI.Services
{
    public partial class ServiceManager
    {
        private class FilterServiceS : IFilterService
        {
            private List<Folder> RootFolders { get; set; }
            private List<List<Folder>> HierarchyLevel { get; set; }
            private List<String> SelectedFolderNames { get; set; }
            private List<Folder> SelectedFolders { get; set; }
            private List<Sensor> FilteredSensorCollection { get; set; }
            private readonly object FilteredSensorCollectionLock = new object();
            private bool isFilterUpdated = false;
            public event EventHandler FilterStateUpdated;
            public bool IsAtRootLevel { get; private set; } = true;

            public FilterServiceS()
            {
                RootFolders = null;
                HierarchyLevel = new List<List<Folder>>();
                SelectedFolderNames = new List<String>();
                SelectedFolders= new List<Folder>();
            }

            public List<String> GetSelectedFoldersHirearchy()
            {
                return SelectedFolderNames;
            }

            public void InitCollections(InnoviObjectCollection<Folder> i_FolderCollection,
                                        InnoviObjectCollection<Sensor> i_SensorCollection)
            {
                if (i_FolderCollection != null)
                    RootFolders = i_FolderCollection.ToList();
                if (i_SensorCollection != null)
                    FilteredSensorCollection = i_SensorCollection.ToList();
                FetchSelectedFolder();
                isFilterUpdated = false;
            }

            protected virtual void OnFilterStateUpdated()
            {
                FilterStateUpdated?.Invoke(this, EventArgs.Empty);
            }

            public IEnumerator GetFilteredEventsEnumerator()
            {
                IEnumerator res = null;

                if (!IsAtRootLevel && SelectedFolders != null && SelectedFolders.Count >= 1)
                {
                    res = SelectedFolders[SelectedFolders.Count - 1].FolderEvents.GetEnumerator();
                }
                else
                {
                    res = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountEvents().GetEnumerator();
                }

                return res;
            }

            public void FetchSelectedFolder()
            {
                if (SelectedFolders != null && SelectedFolders.Count >= 1)
                {
                    FetchSelectedFolderHelper(SelectedFolders[SelectedFolders.Count - 1]);
                }
                else
                {
                    foreach(Folder f in RootFolders)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            FetchSelectedFolderHelper(f);
                        }
                        );
                    }
                }
            }

            private void FetchSelectedFolderHelper(Folder i_currentlyFetchedFolder)
            {
                FilteredSensorCollection = new List<Sensor>();

                if (i_currentlyFetchedFolder.Folders.IsEmpty())  //leaf folder
                {
                    Task.Factory.StartNew(() =>
                    {
                        lock(FilteredSensorCollectionLock)
                        {
                            FilteredSensorCollection.AddRange(i_currentlyFetchedFolder.Sensors.ToList());
                            OnFilterStateUpdated();
                        }
                    });
                }
                else
                {
                    foreach (Folder f in i_currentlyFetchedFolder.Folders)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            FetchSelectedFolderHelper(f);
                        });
                    }
                }
            }

            public List<Sensor> GetFilteredSensorCollection()
            {
                return FilteredSensorCollection;
            }

            public IEnumerator GetFilteredSensorsEnumerator()
            {
                IEnumerator res = null;

                if (!IsAtRootLevel && SelectedFolders != null && SelectedFolders.Count >= 1)
                {
                    res = SelectedFolders[SelectedFolders.Count - 1].GetFilteredSensors().GetEnumerator();
                }
                else
                {
                    res = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountSensors().GetEnumerator();
                }

                return res;
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
                if (i_selectedFolder.Depth == HierarchyLevel.Count - 2)
                {
                    SelectedFolderNames.RemoveRange(i_selectedFolder.Depth, SelectedFolderNames.Count - 1);
                    SelectedFolders.RemoveRange(i_selectedFolder.Depth, SelectedFolders.Count - 1);
                    isLastFiltrationLevelSelection = true;
                }
                else if (i_selectedFolder.Depth < HierarchyLevel.Count - 1)
                {
                    HierarchyLevel.RemoveRange(i_selectedFolder.Depth + 1, HierarchyLevel.Count - 1);
                    SelectedFolderNames.RemoveRange(i_selectedFolder.Depth, SelectedFolderNames.Count);
                    SelectedFolders.RemoveRange(i_selectedFolder.Depth, SelectedFolders.Count);
                }
                if (i_selectedFolder != null)
                {
                    if (i_selectedFolder.Folders != null && !isLastFiltrationLevelSelection)
                    {
                        HierarchyLevel.Add(i_selectedFolder.Folders.ToList());
                    }
                    SelectedFolderNames.Add(i_selectedFolder.Name);
                    SelectedFolders.Add(i_selectedFolder);

                }
                isFilterUpdated = false;
                if(SelectedFolders.Count>=1)
                {
                    IsAtRootLevel = false;
                }
                else
                {
                    IsAtRootLevel = true;
                }
                return HierarchyLevel[HierarchyLevel.Count - 1];
            }

            public List<Folder> GetAccountFolders(User i_user)
            {
                if (RootFolders == null)
                {
                    RootFolders = i_user.GetDefaultAccountFolders().ToList();
                    if (RootFolders == null) { throw new Exception("No folders for current user."); }
                    HierarchyLevel.Add(RootFolders);
                }
                return RootFolders;
            }

            public bool IsEmptyFolder(Folder i_SelectedFolder)
            {
                return i_SelectedFolder.Folders.IsEmpty();
            }
        }
    }
}
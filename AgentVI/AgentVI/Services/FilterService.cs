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
    public partial class ServiceManager
    {
        private class FilterServiceS : IFilterService
        {
            public Account CurrentAccount { get; private set; }
            public List<Account> UserAccounts { get; private set; }
            public IEnumerator FilteredSensorCollection { get; private set; }
            public IEnumerator CurrentLevel { get; private set; }
            public IEnumerator FilteredEvents { get; private set; }
            public bool IsAtRootLevel { get; private set; }
            public bool IsAtLeafFolder { get; private set; }
            public bool HasNextLevel => !IsAtLeafFolder;
            public List<Folder> CurrentPath { get; private set; }
            public Dictionary<Folder, IEnumerator> NextLevel { get; private set; }
            private IEnumerator RootFolders { get; set; }
            public event EventHandler FilterStateUpdated;

            public FilterServiceS()
            {
                IsAtRootLevel = true;
                IsAtLeafFolder = false;
                CurrentAccount = null;
                UserAccounts = null;
                FilteredSensorCollection = null;
                RootFolders = null;
                CurrentPath = new List<Folder>();
                CurrentLevel = null;
                NextLevel = new Dictionary<Folder, IEnumerator>();
            }

            public bool InitServiceModule(User i_User = null)
            {
                bool res = false;
                if(ServiceManager.Instance.LoginService != null &&
                    ServiceManager.Instance.LoginService.LoggedInUser != null)
                {
                    UserAccounts = ServiceManager.Instance.LoginService.LoggedInUser.Accounts;
                    CurrentAccount = ServiceManager.Instance.LoginService.LoggedInUser.Accounts[0];
                    IsAtRootLevel = true;
                    FilteredSensorCollection = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountSensors().GetEnumerator();
                    RootFolders = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountFolders().GetEnumerator();
                    CurrentPath = new List<Folder>();
                    CurrentLevel = RootFolders;
                    fetchNextLevel();                                   //keeps IsAtLeafFolder updated
                    updateFilteredEvents();                             //keeps FilteredEvents updated
                    OnFilterStateUpdated();
                    res = true;
                }

                return res;
            }

            public void SelectRootLevel()
            {
                FilteredSensorCollection = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountSensors().GetEnumerator();
                IsAtRootLevel = true;
                CurrentPath = new List<Folder>();
                CurrentLevel = RootFolders;
                fetchNextLevel();    //keeps IsAtLeafFolder, NextLevel updated
                updateFilteredEvents();
                OnFilterStateUpdated();
            }

            public void SelectFolder(Folder i_FolderSelected)
            {
                FilteredSensorCollection = i_FolderSelected.GetAllSensors().GetEnumerator();
                IsAtLeafFolder = i_FolderSelected.Folders.IsEmpty();
                updatePath(i_FolderSelected);                               //keeps CurrentPath, CurrentPathStr updated
                CurrentLevel = i_FolderSelected.Folders.GetEnumerator();
                fetchNextLevel();                                           //keeps NextLevel updated
                updateFilteredEvents();
                if (i_FolderSelected.Depth >= 0)
                {
                    IsAtRootLevel = false;
                }
                OnFilterStateUpdated();
            }

            public void SwitchAccount(Account i_SelectedAccount)
            {
                i_SelectedAccount.SetAsDefaultAccount();
                CurrentAccount = i_SelectedAccount;
                RootFolders = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountFolders().GetEnumerator();
                CurrentPath = new List<Folder>();
                SelectRootLevel();
            }

            private void fetchNextLevel()
            {
                NextLevel = new Dictionary<Folder, IEnumerator>();
                bool hasNext, wasUpdated = false;
                Folder currentFolder;

                hasNext = RootFolders.MoveNext();
                do
                {
                    if (hasNext == true)
                    {
                        currentFolder = RootFolders.Current as Folder;
                        NextLevel.Add(currentFolder, currentFolder.Folders.GetEnumerator());
                        if (!wasUpdated && !currentFolder.Folders.IsEmpty())
                        {
                            IsAtLeafFolder = false;
                            wasUpdated = true;
                        }
                    }
                    else
                    {
                        IsAtLeafFolder = wasUpdated = true;
                    }
                } while (hasNext = RootFolders.MoveNext());
                RootFolders.Reset();
            }

            private void updatePath(Folder i_FolderSelected)
            {
                if(i_FolderSelected.Depth==0)
                {
                    CurrentPath = new List<Folder>() { i_FolderSelected };
                }
                else if(i_FolderSelected.Depth == CurrentPath.Count)
                {
                    CurrentPath.Add(i_FolderSelected);
                }
                else if(i_FolderSelected.Depth < CurrentPath.Count)
                {
                    CurrentPath.RemoveRange(i_FolderSelected.Depth, CurrentPath.Count-1);
                    CurrentPath.Add(i_FolderSelected);
                }
                else    //i_FolderSelected.Depth > CurrentPath.Count is impossible scenario! something bad happened!
                {
                    throw new Exception("Occured in FilterService.selectFolder");
                }
            }

            private void updateFilteredEvents()
            {
                if(IsAtRootLevel)
                {
                    FilteredEvents = ServiceManager.Instance.LoginService.LoggedInUser.GetDefaultAccountEvents().GetEnumerator();
                }
                else
                {
                    FilteredEvents = CurrentPath[CurrentPath.Count - 1].FolderEvents.GetEnumerator();
                }
            }

            protected virtual void OnFilterStateUpdated()
            {
                FilterStateUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
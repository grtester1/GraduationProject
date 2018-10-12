using InnoviApiProxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Models
{
    public class FolderModel
    {
        private FolderModel(){}
        public string FolderName { get; private set; }
        public bool IsNextFiltrationLevelAvailable { get; private set; }
        public IEnumerator NextLevel { get; private set; }
        public Folder ProxyFolder { get; private set; }

        public static FolderModel FactoryMethod(Folder i_Folder)
        {
            return new FolderModel()
            {
                ProxyFolder = i_Folder,
                FolderName = i_Folder.Name,
                IsNextFiltrationLevelAvailable = !i_Folder.Folders.IsEmpty(),
                NextLevel = i_Folder.Folders.GetEnumerator()
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class Account : InnoviObject
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public eAccountStatus Status { get; set; }
 //       public InnoviObjectCollection<CustomerFolder> CustomerFolders { get; set; }
        public List<Folder> Folders { get; set; }

        public enum eAccountStatus
        {
            Active = 0,
            Undefined,
            Suspended
        }
    }
}
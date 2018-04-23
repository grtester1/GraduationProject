using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Account : InnoviObject
    {
        [JsonPropertyAttribute]
        public string AccountId { get;  private set; }
        [JsonPropertyAttribute]
        public string Name { get; private set; }
        [JsonPropertyAttribute]
        public eAccountStatus Status { get; private set; }
        //       public InnoviObjectCollection<CustomerFolder> CustomerFolders { get; set; }
        [JsonPropertyAttribute]
        public List<Folder> Folders { get; private set; }

        public enum eAccountStatus
        {
            Active = 0,
            Undefined,
            Suspended
        }

        internal Account() { }
    }
}
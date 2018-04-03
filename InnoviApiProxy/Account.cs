using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public class Account : InnoviObject
    {
        private int m_AccountId = 0;
        public string Name { get; set; }
        public eAccountStatus Status { get; set; }
        public InnoviObjectCollection<CustomerFolder> CustomerFolders { get; set; }

        public void AddCustomerFolder(CustomerFolder i_CustomerFolder)
        {
            throw new Exception("Not yet implemented");
        }

        public enum eAccountStatus
        {
            Active = 0,
            Inactive = 1
        }
    }
}

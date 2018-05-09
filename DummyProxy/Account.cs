using System;
using System.Collections.Generic;
using System.Text;

namespace DummyProxy
{
    public class Account
    {
   
        internal int accountId { get;  set; }

        public string Name { get; internal set; }

        public eAccountStatus Status { get; internal set; }

        public enum eAccountStatus
        {
            Active = 0,
            Undefined,
            Suspended
        }

        public void SetAsDefaultAccount()
        {
        
        }

        internal Account() { }
    }
}

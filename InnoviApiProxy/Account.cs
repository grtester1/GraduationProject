using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InnoviApiProxy
{
    public class Account : InnoviObject
    {
        [JsonProperty("id")]
        private int accountId { get; set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public eAccountStatus Status { get; private set; }
        protected override int Id => accountId;

        public enum eAccountStatus
        {
            Active = 0,
            Undefined,
            Suspended
        }

        public void SetAsDefaultAccount()
        {
            HttpUtils.SwitchAccount(accountId);
        }

        internal Account() { }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Services
{
    public class ServiceManager
    {
        private static ServiceManager _Instance = null;
        public static ServiceManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ServiceManager();
                }
                return _Instance;
            }
        }

        public ILoginService LoginService { get; private set; }
        public IFilterService FilterService { get; private set; }


        private ServiceManager()
        {
            LoginService = new LoginService();
            FilterService = new FilterService();
        }
    }
}


using DummyProxy;

//using InnoviApiProxy;

using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Utils
{
    public static class ProxyUtils
    {
        public static bool Compare(this User i_FirstUser, User i_SecondUser)
        {
            return i_FirstUser.Username == i_SecondUser.Username
                && i_FirstUser.UserEmail == i_SecondUser.UserEmail;
        }
    }
}

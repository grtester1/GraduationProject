using System;
using InnoviApiProxy;

namespace NadavTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginResult loginResult = User.Login("ramot.n@gmail.com", "password");
            
        }
    }
}

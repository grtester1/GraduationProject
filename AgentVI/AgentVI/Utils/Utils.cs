#if DPROXY
using DummyProxy;
#else
using InnoviApiProxy;
#endif
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgentVI.Utils
{
    public static class Utils
    {
        public static bool Compare(this User i_FirstUser, User i_SecondUser)
        {
            return i_FirstUser.Username == i_SecondUser.Username
                && i_FirstUser.UserEmail == i_SecondUser.UserEmail;
        }

        public static Point getCoordinates(this VisualElement i_Element)
        {
            Point res = new Point();

            do
            {
                res.X += i_Element.X;
                res.Y += i_Element.Y;
            } while ((i_Element = i_Element.ParentView) != null);

            return res;
        }
    }
}

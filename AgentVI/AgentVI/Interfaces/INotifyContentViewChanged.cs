using AgentVI.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVI.Interfaces
{
    public interface INotifyContentViewChanged
    {
        event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;
    }
}

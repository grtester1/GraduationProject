using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    public delegate List<InnoviObject> InnoviObjectDelegate<InnoviObject>(int i_FilterItemId, int i_PageId, out int i_PagesCount);
  
}

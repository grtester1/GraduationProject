using System;
using System.Collections.Generic;
using System.Text;

namespace AgentVIProxy
{
    public class CustomerFolder : InnoviObject
    {
        private int m_FolderId;
        private int m_ParentId;
        private int m_AccountId;
        private int m_Depth;

        public InnoviObjectCollection<SiteFolder> SiteFolders { get; set; }
        public List<Coordinate> GeoArea;
        public string Name { get; set; }
    }
}

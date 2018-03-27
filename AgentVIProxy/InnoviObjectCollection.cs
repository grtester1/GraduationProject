using System.Collections;
using System.Collections.Generic;

namespace AgentVIProxy
{
    public class InnoviObjectCollection<InnoviObject> : IEnumerable
    {
        private Dictionary<string, InnoviObject> m_InnoviObjectCollection = new Dictionary<string, InnoviObject>();

        public InnoviObjectCollection(Dictionary<string, InnoviObject> i_Collection)
        {
            m_InnoviObjectCollection = i_Collection;
        }
        public int Count
        {
            get { return m_InnoviObjectCollection.Count; }
        }

        public InnoviObject this[string i_Name]
        {
            get { return m_InnoviObjectCollection[i_Name]; }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var innoviObject in m_InnoviObjectCollection)
            {
                yield return innoviObject;
            }
        }
    }
}

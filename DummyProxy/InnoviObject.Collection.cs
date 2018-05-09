using System;
using System.Collections;
using System.Collections.Generic;

namespace DummyProxy
{
    public class InnoviObjectCollection<InnoviObject> : IEnumerable
    {
        private List<InnoviObject> m_Collection = null;

        internal InnoviObjectCollection()
        {
        
        }

        internal InnoviObjectCollection(List<InnoviObject> I_Collection)
        {
            m_Collection = I_Collection;
        }

        public IEnumerator GetEnumerator()
        {
           foreach (InnoviObject innoviObject in m_Collection)
            {
                yield return innoviObject;
            }
        }


        public List<InnoviObject> ToList()
        {

            return m_Collection;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InnoviApiProxy
{
    public class InnoviObjectCollection<InnoviObject> : IEnumerable
    {
        private InnoviObjectDelegate<InnoviObject> m_Delegate;
        private int m_FilterItemId;

        internal InnoviObjectCollection(InnoviObjectDelegate<InnoviObject> i_Delegate)
        {
            m_Delegate = i_Delegate;
        }

        internal InnoviObjectCollection(InnoviObjectDelegate<InnoviObject> i_Delegate, int i_FilterItemId)
        {
            m_Delegate = i_Delegate;
            m_FilterItemId = i_FilterItemId;
        }

        public IEnumerator GetEnumerator()
        {
            return new InnoviObjectCollectionEnumerator(m_Delegate, m_FilterItemId);
        }


        public List<InnoviObject> ToList()
        {
            List<InnoviObject> list = new List<InnoviObject>();

            foreach (InnoviObject innoviObject in this)
            {
                list.Add(innoviObject);
            }

            return list;
        }
        internal class InnoviObjectCollectionEnumerator : IEnumerator
        {
            private int m_CurrentIndex = -1;
            private int m_CurrentPage = 0;
            private int m_TotalPages = 0;

            private List<InnoviObject> m_Collection;
            private InnoviObjectDelegate<InnoviObject> m_Delegate;

            private int m_FilterItemId;

            internal InnoviObjectCollectionEnumerator(InnoviObjectDelegate<InnoviObject> i_Delegate, int i_FilterItemId)
            {
                m_Delegate = i_Delegate;
                m_FilterItemId = i_FilterItemId;
            }

            public object Current
            {
                get
                {
                    return m_Collection[m_CurrentIndex];
                }
            }

            public bool MoveNext()
            {
                m_CurrentIndex++;
               
                if (m_Collection == null || m_CurrentIndex == m_Collection.Count)
                {
                    if (m_Collection == null)
                    {
                        m_Collection = new List<InnoviObject>();
                    }

                    m_CurrentPage++;

                    List<InnoviObject> currentSection = m_Delegate(m_FilterItemId, m_CurrentPage, out m_TotalPages);

                    if (currentSection != null)
                    {
                        m_Collection.AddRange(currentSection);
                    }  
                }

                return m_CurrentPage <= m_TotalPages;
            }

            public void Reset()
            {
                m_CurrentIndex = -1;
                m_CurrentPage = 0;
                m_TotalPages = 0;
            }
        }
    }
}

        

      

       

        


     
        

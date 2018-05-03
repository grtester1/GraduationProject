using System;
using System.Collections.Generic;
using System.Text;

namespace InnoviApiProxy
{
    internal class Cache
    {
        private static Cache m_Instance;
        private Dictionary<int, string> m_SensorCache;

        private Cache()
        {
            m_SensorCache = new Dictionary<int, string>();
        }

        internal static Cache Fetch()
        {
            if (m_Instance == null)
            {
                m_Instance = new Cache();
            }

            return m_Instance;
        }

        internal string GetSensorName(int i_SensorId)
        {
            string sensorName = String.Empty;

            if (m_SensorCache.ContainsKey(i_SensorId))
            {
                sensorName = m_SensorCache[i_SensorId];
            }
            else
            {
                sensorName = HttpUtils.GetSensorNameById(i_SensorId);
                m_SensorCache.Add(i_SensorId, sensorName);
            }

            return sensorName;
        }

        internal void UpdateSensorCache(List<int> i_SensorIds)
        {
            HttpUtils.UpdateSensorNamesCache(i_SensorIds);
        }

        internal void AddToSensorCache(int i_SensorId, string i_SensorName)
        {
            if (!m_SensorCache.ContainsKey(i_SensorId))
            {
                m_SensorCache.Add(i_SensorId, i_SensorName);
            }
        }
    }
}

/*
 * FileName             : AOIInterests.cs
 * Author               : yqs
 * Creat Date           : 2019.2.16
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 感兴趣物体的集合
    /// </summary>
    public class EntityCollection<T> : IEnumerable<MYXZEntity> where T : IEnumerable<MYXZEntity>
    {
        /// <summary>
        /// 此处list较大概率频繁修改，故使用LinkedList
        /// </summary>
        private LinkedList<T> m_list;

        public EntityCollection()
        {
            m_list = new LinkedList<T>();
        }

        public void Add(T item)
        {
            m_list.AddFirst(item);
        }

        public void Remove(T item)
        {
            m_list.Remove(item);
        }

        public void Clear()
        {
            m_list.Clear();
        }

        public IEnumerator<MYXZEntity> GetEnumerator()
        {
            foreach (T item in m_list)
            {
                foreach (MYXZEntity entity in item)
                {
                    yield return entity;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
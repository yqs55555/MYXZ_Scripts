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
    public class AOIInterests : IEnumerable<GameObject>
    {
        private List<MYXZGrid> m_interestGrids;

        public AOIInterests()
        {
            m_interestGrids = new List<MYXZGrid>();
        }

        public void Add(MYXZGrid grid)
        {
            m_interestGrids.Add(grid);
        }

        public void Remove(MYXZGrid grid)
        {
            m_interestGrids.Remove(grid);
        }

        public void Clear()
        {
            m_interestGrids.Clear();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            for (int i = 0; i < m_interestGrids.Count; i++)
            {
                foreach (MYXZEntity entity in m_interestGrids[i])
                {
                    yield return entity.Transform.gameObject;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
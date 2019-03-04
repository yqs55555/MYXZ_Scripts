/*
 * FileName             : Grid.cs
 * Author               : zsz
 * Creat Date           : 2018.10.7
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// Grid类，管理格子信息
    /// </summary>
    public class MYXZGrid : ICollection<MYXZEntity>
    {
        /// <summary>
        ///格子的位置
        /// </summary>
        private readonly Vector2Int m_gridPosition;
        public Vector2Int Position
        {
            get { return this.m_gridPosition; }
        }

        /// <summary>
        ///格子中拥有的实体的表
        /// </summary>
        private HashSet<MYXZEntity> m_existEntityList;
        public MYXZGrid(Vector2Int position)
        {
            this.m_gridPosition = position;
            this.m_existEntityList = new HashSet<MYXZEntity>();
        }

        public int Count
        {
            get { return m_existEntityList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(MYXZEntity entity)
        {
            m_existEntityList.Add(entity);
        }

        public void Clear()
        {
            m_existEntityList.Clear();
        }

        public bool Contains(MYXZEntity entity)
        {
            return this.m_existEntityList.Contains(entity);
        }

        public void CopyTo(MYXZEntity[] array, int arrayIndex)
        {
            throw new NotSupportedException("不支持此方法");
        }

        public bool Remove(MYXZEntity entity)
        {
            return this.m_existEntityList.Remove(entity);
        }

        public IEnumerator<MYXZEntity> GetEnumerator()
        {
            return this.m_existEntityList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

/*
 * FileName             : MapModel.cs
 * Author               : yqs
 * Creat Date           : 2019.2.18
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
    public class SceneModel
    {
        private Dictionary<GameObject, MYXZEntity> m_gameObject2Entity;

        public SceneModel()
        {
            m_gameObject2Entity = new Dictionary<GameObject, MYXZEntity>();
        }

        public void AddEntity(GameObject go, MYXZEntity entity)
        {
            m_gameObject2Entity.Add(go, entity);
        }

        public bool Remove(GameObject go)
        {
            return this.m_gameObject2Entity.Remove(go);
        }

        public MYXZEntity this[GameObject go]
        {
            get
            {
                return m_gameObject2Entity.ContainsKey(go) ? m_gameObject2Entity[go] : null;
            }
        }
    }
}
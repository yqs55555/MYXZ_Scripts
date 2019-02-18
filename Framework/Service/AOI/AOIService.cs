/*
 * FileName             : AOIService.cs
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
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// AOIService，管理AOI信息
    /// </summary>
    public class AOIService
    {
        [Inject]
        public AOIInfoModel AOIInfoModel { get; set; }

        /// <summary>
        /// 这个Entity感兴趣的物体的迭代器，按需创建
        /// </summary>
        private Dictionary<MYXZEntity, IEnumerable<GameObject>> m_entityInterests;

        public AOIService()
        {
            m_entityInterests = new Dictionary<MYXZEntity, IEnumerable<GameObject>>();
        }

        /// <summary>
        /// 采用迭代器的方式来返回感兴趣物体列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<GameObject> InterestGameObjects(MYXZEntity entity)
        {
            //采用返回迭代器的方式来返回相较List可以免去频繁查询下的GC

            if (entity.IsDirty)    //当Entity的AOI坐标发生变化后会被标记为Dirty，此后第一次调用此方法需要更新信息
            {
                AOIInterestList entityInterestEnumerable;
                if (m_entityInterests.ContainsKey(entity))  //如果已经创建过此entity的感兴趣物体迭代器
                {
                    entityInterestEnumerable = m_entityInterests[entity] as AOIInterestList;
                    entityInterestEnumerable.Clear();
                    RefreshEntityInterests(entity, entityInterestEnumerable);
                }
                else
                {
                    entityInterestEnumerable = new AOIInterestList();
                    RefreshEntityInterests(entity, entityInterestEnumerable);
                    m_entityInterests.Add(entity, entityInterestEnumerable);
                }
            }

            return m_entityInterests[entity];
        }

        private void RefreshEntityInterests(MYXZEntity entity, AOIInterestList enumerable)
        {
            for (int x = entity.LocatedGridPosition.x - entity.InterstRadius;
                x < 2 * entity.InterstRadius; x++)  //正方形的感兴趣范围
            {
                for (int y = entity.LocatedGridPosition.y - entity.InterstRadius;
                    y < 2 * entity.InterstRadius; y++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    if (AOIInfoModel.Contains(pos)) //如果这个pos在当前地图是合法的
                    {
                        enumerable.Add(AOIInfoModel.AllGrids[pos]);
                    }
                }
            }
        }

        public static Vector2Int CalAoiPosition(Transform transform)
        {
            return new Vector2Int(
                (int)(transform.position.x / Setting.AOI.GRID_FIELD) + (transform.position.x >= 0 ? 1 : -1),
                (int)(transform.position.z / Setting.AOI.GRID_FIELD) + (transform.position.z >= 0 ? 1 : -1)
                );
        }
    }
}

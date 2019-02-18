/*
 * FileName             : AOIInfo.cs
 * Author               : ZSZ
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
using UnityEngine;
using ProtoBuf;


namespace MYXZ
{
    /// <summary>     
    /// AOI的信息
    /// </summary>
    public class AOIInfoModel : IObserver<Vector2Int>
    {
        /// <summary> 
        /// 所有格子 all grids
        /// </summary> 
        public Dictionary<Vector2Int, MYXZGrid> AllGrids;
        /// <summary>
        /// 当前地图的grid的x坐标限制区间，左右均为闭区间，x代表最小值，y代表最大值
        /// </summary>
        private Vector2Int m_xLimit;
        /// <summary>
        /// 当前地图的grid的y坐标限制区间，左右均为闭区间，x代表最小值，y代表最大值
        /// </summary>
        private Vector2Int m_yLimit;

        private Dictionary<MYXZEntity, int> m_entityUpdateMark;

        public AOIInfoModel()
        {
            AllGrids = new Dictionary<Vector2Int, MYXZGrid>(100);
            this.m_entityUpdateMark = new Dictionary<MYXZEntity, int>();
            UpdateMapInfo(new Vector2Int(0,0), new Vector2Int(160,160));
        }

        /// <summary>
        /// 设置当前地图的信息
        /// </summary>
        /// <param name="mapBegin">当前地图起始坐标</param>
        /// <param name="mapEnd">当前地图终止坐标</param>
        public void UpdateMapInfo(Vector2Int mapBegin, Vector2Int mapEnd)
        {
            m_xLimit = new Vector2Int(      //设置当前地图的Grid的X坐标范围
                (int)(mapBegin.x / Setting.AOI.GRID_FIELD) + (mapBegin.x >= 0 ? 1 : -1),
                (int)(mapEnd.x / Setting.AOI.GRID_FIELD) + (mapEnd.x >= 0 ? 1 : -1)
                );
            m_yLimit = new Vector2Int(      //Y坐标范围（Y对应Unity中的Z轴
                (int)(mapBegin.y / Setting.AOI.GRID_FIELD) + (mapBegin.y >= 0 ? 1 : -1),
                (int)(mapEnd.y / Setting.AOI.GRID_FIELD) + (mapEnd.y >= 0 ? 1 : -1)
                );
            for (int i = m_xLimit.x; i <= m_xLimit.y; i++)  //初始化当前地图Grid，左右均为闭区间
            {
                for (int j = m_yLimit.x; j <= m_yLimit.y; j++)
                {
                    Vector2Int pos = new Vector2Int(i,j);
                    if (AllGrids.ContainsKey(pos))      //如果上一个地图中已经存在了此Grid
                    {
                        AllGrids[pos].Clear();
                    }
                    else        //否则，创建一个
                    {
                        MYXZGrid grid = new MYXZGrid(pos);
                        AllGrids.Add(pos, grid);
                    }
                }
            }
        }

        /// <summary>
        /// 当这个AOIInfoModel所管理的MYXZEntity的AOI坐标发生变化时，将会触发此函数
        /// </summary>
        /// <param name="sender">Entity</param>
        /// <param name="args">这个Entity之前所在的坐标</param>
        public void Update(IObservable<Vector2Int> sender, Vector2Int args)
        {
            MYXZEntity entity = (MYXZEntity)sender;
            if (AllGrids.ContainsKey(args))     //如果这个entity之前存在于此Model中
            {
                if (AllGrids[args].Contains(entity))  //移除
                {
                    AllGrids[args].Remove(entity);
                }
            }

            if (!AllGrids.ContainsKey(entity.LocatedGridPosition))  //
            {
                Debug.LogError("正在访问不存在的Gird" + entity.LocatedGridPosition);
                return;
            }

            AllGrids[entity.LocatedGridPosition].Add(entity);
        }

        public bool Contains(Vector2Int position)
        {
            return this.AllGrids.ContainsKey(position);
        }

        public void AddEntityUpdateMark(MYXZEntity entity, int mark)
        {
            this.m_entityUpdateMark.Add(entity, mark);
        }

        private void ShowAllGrids()
        {
            foreach (KeyValuePair<Vector2Int, MYXZGrid> grid in AllGrids)
            {
                Debug.Log(grid.Key + "," + grid.Value);
            }
        }
    }
}

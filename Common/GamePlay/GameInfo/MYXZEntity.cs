/*
 * FileName             : Entity.cs
 * Author               : zsz
 * Creat Date           : 2018.10.6
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace MYXZ
{
    public class MYXZEntity : IObservable<Vector2Int>
    {
        /// <summary>
        /// 所在的格子坐标
        /// </summary>
        private Vector2Int m_locatedGridPosition;
        public Vector2Int LocatedGridPosition
        {
            get { return this.m_locatedGridPosition; }
            private set
            {
                if (m_locatedGridPosition != value)
                {
                    this.IsDirty = true;
                    Vector2Int tmpPosition = m_locatedGridPosition;
                    this.m_locatedGridPosition.Set(value.x, value.y);
                    NotifyObservers(tmpPosition);
                }
            }
        }

        /// <summary>
        /// 这个Entity所指向的目标物体
        /// </summary>
        private Character m_character;
        /// <summary>
        /// 目标GameObject的transform
        /// </summary>
        public Transform Transform
        {
            get { return this.m_character.TargetGameObject.transform; }
        }

        /// <summary>
        /// 当前物体的感兴趣半径
        /// </summary>
        private int m_interestRadius;
        public int InterstRadius
        {
            get { return m_interestRadius; }
            set { m_interestRadius = value; }
        }

        private bool m_isDirty;
        public bool IsDirty
        {
            get { return this.m_isDirty; }
            set { m_isDirty = value; }
        }

        private Action<IObservable<Vector2Int>, Vector2Int> m_positonNotifyEventHandler;

        public MYXZEntity(Character character, int interestRadius)
        {
            this.m_character = character;
            this.m_interestRadius = interestRadius;
            this.m_locatedGridPosition = new Vector2Int(0, 0);
            this.IsDirty = true;
        }

        public void RegisterObserver(Action<IObservable<Vector2Int>, Vector2Int> notifyEventHandler)
        {
            m_positonNotifyEventHandler += notifyEventHandler;
        }

        public void RemoveObserver(Action<IObservable<Vector2Int>, Vector2Int> notifyEventHandler)
        {
            if (m_positonNotifyEventHandler != null)
            {
                try
                {
                    m_positonNotifyEventHandler -= notifyEventHandler;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    throw;
                }
            }
        }

        /// <summary>
        /// 提醒观察者gridPosition更新了
        /// </summary>
        /// <param name="args">旧的gridPosition</param>
        public void NotifyObservers(Vector2Int args)
        {
            m_positonNotifyEventHandler.Invoke(this, args);
        }

        public void UpdatePosition()
        {
            this.LocatedGridPosition = AOIService.CalAoiPosition(this.Transform);
        }
    }
}
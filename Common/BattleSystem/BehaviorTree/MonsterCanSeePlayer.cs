/*
 * FileName             : MyCanSeeObject.cs
 * Author               : hy
 * Creat Date           : 2018.10.3
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace MYXZ
{
    public class MonsterCanSeePlayer : Conditional
    {
        /// <summary>
        /// 判断物体是否在视野内的条件任务
        /// </summary>
        private Transform mPlayer;
        public float fieldOfViewAngle = 150;
        //public float viewDistance = 9;

        public SharedFloat sharedViewDistance;
        public SharedTransform target;

        public override void OnStart()
        {
            mPlayer = GameObject.FindWithTag("Player").transform;
        }

        public override TaskStatus OnUpdate()
        {
            if (mPlayer == null)
                return TaskStatus.Failure;
            float distance = (mPlayer.position - transform.position).magnitude;
            float angle = Vector3.Angle(transform.forward, mPlayer.position - transform.position);
            if (distance < sharedViewDistance.Value && angle < fieldOfViewAngle * 0.5f)
            {
                this.target.Value = mPlayer;
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}
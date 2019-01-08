/*
 * FileName             : MySeek.cs
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
    public class MySeek : Action
    {
        /// <summary>
        /// 寻找目标的动作任务
        /// </summary>
        private Character m_character;
        //public float speed;
        public SharedFloat seekSpeed;
        public SharedTransform target;
        public float arriveDistance;
        private float sqrArriveDistance;
        //public SharedFloat sharedArriveDistance;
        public override void OnStart()
        {
            //sharedArriveDistance = arriveDistance;
            sqrArriveDistance = arriveDistance * arriveDistance;
            this.m_character = this.transform.GetComponent<MonsterView>().Character;
            m_character.Animator.SetBool("walk", true);

        }

        public override TaskStatus OnUpdate()
        {
            if (target == null || target.Value == null)
            {
                return TaskStatus.Failure;
            }
            this.transform.LookAt(target.Value.position);
            this.transform.position = Vector3.MoveTowards(transform.position, target.Value.position, seekSpeed.Value * Time.deltaTime);
            return (target.Value.position - transform.position).sqrMagnitude < sqrArriveDistance ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            m_character.Animator.SetBool("walk", false);
        }


    }
}
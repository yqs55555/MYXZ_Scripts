/*
 * FileName             : NomalAttack.cs
 * Author               : hy
 * Creat Date           : 2018.10.30
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
    public class NomalAttack : Action
    {
        /// <summary>
        /// 正常攻击
        /// </summary>

        public float attackDistance = 1.0f;
        private float sqrAttackDistance;
        public SharedTransform target;
        //public SharedFloat sharedArriveDistance;
        public SharedFloat sharedViewDistance;
        private float sqrViewDistance;
        public SharedFloat seekSpeed;

        public override void OnStart()
        {
            sqrAttackDistance = attackDistance * attackDistance;
            sqrViewDistance = sharedViewDistance.Value * sharedViewDistance.Value;
        }

        public override TaskStatus OnUpdate()
        {
            if ((target.Value.position - transform.position).sqrMagnitude < sqrAttackDistance) {
                this.transform.LookAt(target.Value.position);
                //mView.UseSkill(KeyCode.Alpha1);
                return TaskStatus.Running;
            }
            else if ((target.Value.position - transform.position).sqrMagnitude < sqrViewDistance)
            {
                this.transform.LookAt(target.Value.position);
                this.transform.position = Vector3.MoveTowards(transform.position, target.Value.position, seekSpeed.Value * Time.deltaTime);
                return TaskStatus.Running;
            }
            else
            {
                return TaskStatus.Failure;

            }

        }


    }
}
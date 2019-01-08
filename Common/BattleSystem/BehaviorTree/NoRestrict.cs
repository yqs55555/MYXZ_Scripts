/*
 * FileName             : NoRestrict.cs
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
    public class NoRestrict : Conditional
    {
        /// <summary>
        /// 怪物没有受到玩家控制技能限制
        /// </summary>

        private bool restrict = false;

        public override void OnStart()
        {

        }
        public override TaskStatus OnUpdate()
        {
            if (!restrict)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}

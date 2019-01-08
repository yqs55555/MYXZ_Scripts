/*
 * FileName             : CureSelf.cs
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
    /// <summary>
    /// 疯狂攻击
    /// </summary>
    public class CureSelf : Action
    {


        public override void OnStart()
        {
            this.transform.GetComponent<MonsterView>().Character.Animator.SetBool("cure", true); ;
            //character.Animator.SetBool("attack", false);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

    }

}
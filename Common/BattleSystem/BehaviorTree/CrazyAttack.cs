/*
 * FileName             : CrazyAttack.cs
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
    public class CrazyAttack : Action
    {
        /// <summary>
        /// 疯狂攻击
        /// </summary>

        private Character m_character;

        public override void OnStart()
        {
            this.m_character = this.transform.GetComponent<MonsterView>().Character;
            m_character.Animator.SetBool("crazyAttack", true);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}
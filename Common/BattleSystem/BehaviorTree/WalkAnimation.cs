/*
 * FileName             : WalkAnimation.cs
 * Author               : hy
 * Creat Date           : 2018.11.2
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

    public class WalkAnimation : Action
    {

        private Character m_character;

        public override void OnStart()
        {
            this.m_character = this.transform.GetComponent<MonsterView>().Character;
            m_character.Animator.SetBool("walk", true);

        }

        // Update is called once per frame
        public override TaskStatus OnUpdate()
        {
            m_character.Animator.SetBool("walk", true);
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            m_character.Animator.SetBool("walk", false);
        }
    }
}
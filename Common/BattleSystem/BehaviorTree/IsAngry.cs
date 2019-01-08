/*
 * FileName             : IsAngry.cs
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
using System.Threading;

namespace MYXZ
{
    public class IsAngry : Action
    {
        /// <summary>
        /// 判断怪物是否处于发怒状态
        /// </summary>

        private Character m_character;

        private int fullHp = 100;//满血血量
        private int angerThreshold = 1;//愤怒的血量阈值
        private float angerProbability = 1.0f;//愤怒的随机概率阈值
        private float angerRandom;//愤怒的随机值

        public override void OnStart()
        {
            this.m_character = this.transform.GetComponent<MonsterView>().Character;
            this.m_character.HP = 80;
            angerRandom = Random.Range(0.0f, 1.0f);
        }

        public override TaskStatus OnUpdate()
        {
            m_character.Animator.SetBool("attack", false);

            if (this.m_character.HP <= fullHp * angerThreshold)
            {
                if (angerRandom <= angerProbability)
                {
                    m_character.Animator.SetBool("angry", true);
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }

 
    }
}
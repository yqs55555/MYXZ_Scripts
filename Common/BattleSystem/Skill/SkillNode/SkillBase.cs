﻿/*
 * FileName             : SkillBase.cs
 * Author               : yqs
 * Creat Date           : 2018.9.26
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

namespace MYXZ
{
    /// <summary>
    /// 释放的技能的子技能（即技能的最小单元）,不会有子节点了
    /// </summary>
    [Serializable]
    public class SkillLeaf : SkillNode
    {
        /// <summary>
        /// 此技能的攻击区域
        /// </summary>
        private TargetPicker m_targetPicker;

        public string AnimationName
        {
            get { return mAnimationName; }
        }

        public float SkillTime
        {
            get { return mSkillTime; }
        }
        private readonly string mAnimationName;
        private readonly float mSkillTime;

        public SkillLeaf(string id, TargetPicker targetPicker,
            string animationName, float skillTime) : base(id)
        {
            this.m_targetPicker = targetPicker;
            this.mAnimationName = animationName;
            this.mSkillTime = skillTime;
        }

        public override SkillNodeState Use()
        {
            SkillEnter();
            List<Transform> interests = new List<Transform>();
//            foreach (Entity entity in skillUser.CurrentInterestEntityList)
//            {
//                if (!entity.Transform.Equals(skillUser.Transform))
//                {
//                    interests.Add(entity.Transform);
//                }
//            }

            if (interests.Count == 0)
            {
                interests = null;
            }
//            List<Transform> attackTargets = mTargetPicker.Pick(SkillTree.User.Entity.Transform, interests);
//            if (attackTargets != null)
//            {
//                foreach (Transform attackTarget in attackTargets)
//                {
//                    if (attackTarget.CompareTag("Player") || attackTarget.CompareTag("Monster"))
//                    {
//                        //attackTarget.GetComponent<CharacterView>().BeAttacked(skillUser.Transform, this);
//                    }
//                    //Debug.Log(attackTarget.name + " Get Attacked(怪物AI中怪物受到攻击请在这里设定)");
//                }
//            }
            return SkillNodeState.Finish;      //代表当前技能节点是可以施放的
        }
        /// <summary>
        /// 开始这个技能
        /// </summary>
        private void SkillEnter()
        {
            // skillUser.Transform.GetComponent<Animator>().Play(this.mAnimationName);
            //skillUser.Transform.GetComponent<CharacterView>().Character.SetCurrentSkill(this);
        }

        /// <summary>
        /// 结束这个技能
        /// </summary>
        public void SkillExit()
        {
        }

        /// <summary>
        /// 终止这个技能
        /// </summary>
        public void SkillStop()
        {

        }
    }
}
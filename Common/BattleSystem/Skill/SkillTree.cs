/*
 * FileName             : SkillTree.cs
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
    /// 一颗SkillTree代表一个技能，一个技能可能有多段或者多种效果
    /// </summary>
    [Serializable]
    public class SkillTree
    {
        private readonly SkillRootNode mRootSkillNodes;
        public SkillBase CurrentSkill;
        public KeyCode ShortCut = KeyCode.None;
        public float SkillUsedTime;

        public string ID
        {
            get { return mRootSkillNodes.Id; }
        }

        public SkillTree(string skillTreeId)
        {
            mRootSkillNodes = MYXZGameDataManager.Instance.GetSkillTreeById(skillTreeId);
            CurrentSkill = null;
        }

        public void Use(MYXZEntity entity)
        {
//            if (entity.Transform.GetComponent<CharacterView>().Character.CurrentSkill != null)
//            {
//                return;
//            }
            mRootSkillNodes.Use(entity);
        }
    }
}
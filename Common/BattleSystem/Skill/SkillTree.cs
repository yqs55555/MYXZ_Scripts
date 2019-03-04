﻿/*
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
        /// <summary>
        /// 技能树的根节点
        /// </summary>
        private readonly SkillRootNode mRootSkillNodes;
        /// <summary>
        /// 技能释放时的产生的协程
        /// </summary>
        public int SkillMark;
        public KeyCode ShortCut = KeyCode.None;
        public SkillUser User;

        public string ID
        {
            get { return mRootSkillNodes.Id; }
        }

        public SkillTree(string skillTreeId)
        {
            mRootSkillNodes = MYXZConfigLoader.Instance.GetSkillTree(skillTreeId);
        }

        public void Use()
        {
            
            mRootSkillNodes.Use();
        }

        /// <summary>
        /// 外界试图终止技能
        /// </summary>
        /// <returns>终止成功返回True</returns>
        public bool Stop()
        {
            return false;
        }
    }
}
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
    /*
     * 对于一颗技能树，由于每个节点的子节点数量不确定，所以自然得想到了使用组合模式，
     * 而技能在执行过程中存在两种情况，分别是技能释放条件分析（SkillCondition节点）
     * 和技能的具体释放（SkillLeaf节点），所有节点有三种状态：Running，Finish，FailToRun，
     * 对于SkillCondition，其下有多个子节点，当某个子节点成功执行时（返回Finish时），
     * 此SkillCondition节点也将返回Finish；对于SkillLeaf节点，如果是瞬发的就会直接返回
     * Finish，如果有一定持续时间的，则会返回Running，并且在其结束时通知其父节点Finish，
     * 在其被打断时也会通知其父节点FailToRun或者其他状态（这里应该再添加一个观察者模式）。
     * 而对于根节点而言，会依次执行各个子节点，直到所有子节点执行完毕，进入技能冷却时间。
     *
     */

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
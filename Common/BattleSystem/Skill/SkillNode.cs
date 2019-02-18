﻿/*
 * FileName             : SkillNode.cs
 * Author               : yqs
 * Creat Date           : 2018.9.26
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 技能树的节点
    /// </summary>
    public abstract class SkillNode
    {
        private readonly string m_id;
        protected List<SkillNode> ChildSkillNodes;
        protected int NextUse;

        public string Id
        {
            get { return m_id; }
        }

        protected SkillNode(string id)
        {
            m_id = id;
        }

        public abstract SkillNodeState Use(MYXZEntity skillUser);

        public void AddChildNode(SkillNode skillNode)
        {
            if (ChildSkillNodes == null)
            {
                ChildSkillNodes = new List<SkillNode>();
            }
            ChildSkillNodes.Add(skillNode);
        }
    }
}
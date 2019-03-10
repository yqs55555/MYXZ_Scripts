/*
 * FileName             : SkillCondition.cs
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
    /// 技能树的条件判断节点
    /// </summary>
    public class SkillCondition : SkillNode
    {
        public SkillCondition(string id) : base(id)
        {
            NextUse = 0;
        }

        public override SkillNodeState Use()
        {
            SkillNodeState currentNodeState = SkillNodeState.FailToRun;
            for (int i = 0; i < ChildSkillNodes.Count; i++)
            {
                switch (ChildSkillNodes[NextUse].Use())
                {
                    case SkillNodeState.Running:
                        return SkillNodeState.Running;

                    case SkillNodeState.Finish:                 //此子技能释放完毕
                        currentNodeState = SkillNodeState.Running;          //此节点的子节点正在释放
                        NextUse = (NextUse + 1) % ChildSkillNodes.Count;
                        if (NextUse == 0)                                   //如果刚才释放的是最后一个子节点技能，代表完成
                        {
                            currentNodeState = SkillNodeState.Finish;
                        }
                        break;
                    case SkillNodeState.FailToRun:          //当子技能无法被释放时
                        NextUse = (NextUse + 1) % ChildSkillNodes.Count;
                        break;

                    default:
                        NextUse = (NextUse + 1) % ChildSkillNodes.Count;
                        break;
                }
            }
            return currentNodeState;
        }
    }
}
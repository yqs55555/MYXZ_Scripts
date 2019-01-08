/*
 * FileName             : BattleSystemManager.cs
 * Author               : yqs
 * Creat Date           : 2019.1.5
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MYXZ
{
    public class BattleSystemManager : Singleton<BattleSystemManager>
    {
        private Dictionary<ISkillUseable, List<SkillTree>> m_skillUsers = new Dictionary<ISkillUseable, List<SkillTree>>();

        void Update()
        {
            foreach (var skillUser in m_skillUsers)
            {
                foreach (SkillTree skillTree in skillUser.Value)
                {

                }
            }
        }

        public void AddSkill(ISkillUseable skillUser, SkillTree skill)
        {
            if (m_skillUsers.Keys.Contains(skillUser))
            {
                m_skillUsers[skillUser].Add(skill);
            }
            else
            {
                m_skillUsers.Add(skillUser, new List<SkillTree>() { skill });
            }
        }
    }
}
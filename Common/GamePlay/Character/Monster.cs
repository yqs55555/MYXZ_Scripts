/*.
 * FileName             : Monster.cs
 * Author               : yqs
 * Creat Date           : 2019.1.6
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
    public class Monster : Character, ISkillUseable
    {
        private bool m_isUsingSkill = false;

        public Monster(GameObject gameObject) : base(gameObject)
        {
        }

        public bool IsUsingSkill
        {
            get { return m_isUsingSkill; }
        }

        public void UseSkill(SkillTree skill)
        {
        }

        public void StopSkill()
        {
        }
    }
}
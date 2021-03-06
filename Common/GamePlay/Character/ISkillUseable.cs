﻿/*
 * FileName             : ISkillUseable.cs
 * Author               : yqs
 * Creat Date           : 2018.1.5
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
    /// 可以释放技能
    /// </summary>
    public interface ISkillUseable
    {
        bool IsUsingSkill { get; }
        void UseSkill(SkillTree skill);
        void StopSkill();
    }
}
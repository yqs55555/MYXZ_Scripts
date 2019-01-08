/*
 * FileName             : ICanBeTalkTo.cs
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
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 可以被谈话
    /// </summary>
    public interface ICanBeTalkTo
    {
        bool IsTalking { get; }
        void TalkTo(ICanBeTalkTo talker);
        void TalkFinish();
    }
}
/*
 * FileName             : IUseFSM.cs
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
    public interface IUseFSM
    {
        StateID CurrentStateID { get; }
        void FixedUpdate();
    }
}
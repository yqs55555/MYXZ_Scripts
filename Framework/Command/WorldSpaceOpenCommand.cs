/*
 * FileName             : WorldSpaceOpenCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.1.30
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Diagnostics;
using strange.extensions.command.impl;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MYXZ
{
    /// <summary>
    /// 世界场景被打开时调用，做初始化,Bind From WorldSpaceOpenSignal
    /// </summary>
    public class WorldSpaceOpenCommand : Command
    {
        public override void Execute()
        {
            Setting.Init();
            MYXZUIManager.Instance.PopAllPanel();
            MYXZInput.IsEnable = true;
            MYXZConfigLoader.Instance.Init(); 
            //ReqLoadArchiveSignal.Dispatch();
        }
    }
}
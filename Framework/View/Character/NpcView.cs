/*
 * FileName             : NpcView.cs
 * Author               : yqs
 * Creat Date           : 2018.2.12
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>     
    /// NPC的View
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class NpcView : View
    {
        public string ID;
        public string Name;
        public NPC NPC;

        /// <summary>
        /// 开始谈话的Signal
        /// </summary>
        public Signal BeginTalkSignal = new Signal();

        protected override void Start()
        {
            base.Start();
            bl_HUDText.Instance.SetHUDText(this.Name, transform, Color.blue, bl_Guidance.Static, 5);
        }
    }
}
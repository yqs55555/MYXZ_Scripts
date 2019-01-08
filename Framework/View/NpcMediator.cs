/*
 * FileName             : NpcMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.2.20
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// NPC的Mediator
    /// </summary>
    public class NpcMediator : Mediator
    {
        [Inject]
        public NpcView View { get; set; }

        [Inject]
        public BeginTalkSignal BeginTalkSignal { get; set; }

        [Inject]
        public RefreshAOISignal RefreshAoiSignal { get; set; }

        private int mTimer = -1;

        public override void OnRegister()
        {
            this.View.NPC = new NPC(this.gameObject);
            View.BeginTalkSignal.AddListener(RequestTalk);
            if (View.NPC.IsStaticNPC)
            {
                RefreshAoiSignal.Dispatch(this.transform);
            }
            else
            {
                mTimer = 0;
            }
        }

        public override void OnRemove()
        {
            View.BeginTalkSignal.RemoveListener(RequestTalk);
        }

        private void RequestTalk()
        {
            BeginTalkSignal.Dispatch(View.ID);
        }

        void FixedUpdate()
        {
            if (mTimer == 0)
            {
                RefreshAoiSignal.Dispatch(this.transform);
            }
            mTimer = (mTimer + 1) % 10;
        }
    }
}
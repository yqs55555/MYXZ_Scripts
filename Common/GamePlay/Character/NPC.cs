/*
 * FileName             : NPC.cs
 * Author               : yqs
 * Creat Date           : 2019.1.5
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    [Serializable]
    public class NPC : Character, ICanBeTalkTo
    {
        public bool IsStaticNPC = true;
        public List<Transform> PatrolPoints;

        private bool m_isTalking = false;
        private IUseFSM m_npcFsm;

        public NPC(GameObject gameObject) : base(gameObject)
        {
            m_npcFsm = new NpcFsm(this);
        }

        public bool IsTalking
        {
            get { return m_isTalking; }
        }

        public void TalkTo(ICanBeTalkTo talker)
        {
            if (!this.m_isTalking && !talker.IsTalking)
            {
                m_isTalking = true;
                this.TargetGameObject.GetComponent<NpcView>().BeginTalkSignal.Dispatch();
            }
        }

        public void TalkFinish()
        {
            m_isTalking = false;
        }

        public override void FixedUpdate()
        {
            m_npcFsm.FixedUpdate();
        }
    }
}
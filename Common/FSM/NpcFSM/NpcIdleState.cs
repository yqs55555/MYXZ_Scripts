/*
 * FileName             : NpcIdleState.cs
 * Author               : 
 * Creat Date           : 
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
    /// NPC的idle状态，StateID为IdleState
    /// </summary>
    public class NpcIdleState : FSMState
    {
        private NPC m_npc;

        public NpcIdleState(FSMSystem fsm, NPC npc) : base(fsm)
        {
            this.StateID = StateID.IdleState;
            this.m_npc = npc;
        }

        public override void StateAction(GameObject[] gameObjects)
        {
        }

        public override void Reason(GameObject[] gameObjects)
        {
            if (m_npc.IsTalking) //如果NPC正在谈话
            {
                this.Fsm.PerformTransition(Transition.ReadytoChat); //进入NpcChatState
            }
        }
    }
}
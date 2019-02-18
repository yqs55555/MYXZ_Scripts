/*
 * FileName             : NpcFsm.cs
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
    public class NpcFsm : IUseFSM
    {
        private FSMSystem m_fsm;
        private NPC m_npc;

        public StateID CurrentStateID
        {
            get { return m_fsm.CurrentStateID; }
        }

        public NpcFsm(NPC npc)
        {
            this.m_npc = npc;
            InitFsm();
        }

        public void FixedUpdate()
        {
            m_fsm.UpdateFSM(null);
        }

        private void InitFsm()
        {
            m_fsm = new FSMSystem(this.m_npc.TargetGameObject);

            if (this.m_npc.IsStaticNPC)
            {
                NpcIdleState idleState = new NpcIdleState(m_fsm, this.m_npc);
                idleState.AddTransition(Transition.ReadytoChat, StateID.ChatState);

                NpcChatState chatState = new NpcChatState(m_fsm, this.m_npc);
                chatState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);

                m_fsm.AddStates(idleState, chatState);
            }
            else
            {
                NpcPatrolState patrolState = new NpcPatrolState(m_fsm, this.m_npc);
                patrolState.AddTransition(Transition.ReadytoChat, StateID.ChatState);

                NpcChatState chatState = new NpcChatState(m_fsm, this.m_npc);
                chatState.AddTransition(Transition.ReadytoPatrol, StateID.PatrolState);

                m_fsm.AddStates(patrolState, chatState);
            }
        }
    }
}
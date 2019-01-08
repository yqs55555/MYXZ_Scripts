/*
 * FileName             : PlayerFsm.cs
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
    public class PlayerFsm : IUseFSM
    {
        private FSMSystem m_fsm;
        private Player m_player;

        public StateID CurrentStateID
        {
            get { return this.m_fsm.CurrentStateID; }
        }

        public PlayerFsm(Player player)
        {
            this.m_player = player;
            InitFsm();
        }

        private void InitFsm()
        {
            this.m_fsm = new FSMSystem(this.m_player.TargetGameObject);
            IdleState idleState = new IdleState(m_fsm, this.m_player);
            idleState.AddTransition(Transition.ReadytoWalk, StateID.WalkState);
            idleState.AddTransition(Transition.ReadytoChat, StateID.ChatState);
            idleState.AddTransition(Transition.ReadytoJump, StateID.JumpState);
            idleState.AddTransition(Transition.ReadytoRun, StateID.RunState);

            WalkState walkState = new WalkState(m_fsm, this.m_player);
            walkState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);
            walkState.AddTransition(Transition.ReadytoChat, StateID.ChatState);
            walkState.AddTransition(Transition.ReadytoJump, StateID.JumpState);
            walkState.AddTransition(Transition.ReadytoRun, StateID.RunState);

            ChatState chatState = new ChatState(m_fsm, this.m_player);
            chatState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);

            JumpState jumpState = new JumpState(m_fsm, this.m_player);
            jumpState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);

            RunState runState = new RunState(m_fsm, this.m_player);
            runState.AddTransition(Transition.ReadytoWalk, StateID.WalkState);
            runState.AddTransition(Transition.ReadytoJump, StateID.JumpState);

            m_fsm.AddStates(idleState, walkState, chatState, jumpState, runState);
        }

        /// <summary>
        /// 此方法需要在monobehavior中调用
        /// </summary>
        public void FixedUpdate()
        {
            this.m_fsm.UpdateFSM(null);
        }
    }
}
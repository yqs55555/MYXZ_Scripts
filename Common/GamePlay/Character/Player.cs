﻿/*
 * FileName             : Character.cs
 * Author               : yqs
 * Creat Date           : 2018.3.14
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
using UnityEngine.EventSystems;
using ProtoBuf;

namespace MYXZ
{
    /// <summary>
    /// 玩家控制的角色的信息
    /// </summary>
    [Serializable]
    public class Player : Character, ICanBeTalkTo, ISkillUseable
    {
        public Equipment Weapon;
        public Equipment Hat;
        public Equipment Shoes;
        public Equipment Ornament1;
        public Equipment Clothes;
        public Equipment Ornament2;

        [Tooltip("距离小于此时可以交谈")]
        public float TalkDistance = 10;
        [SerializeField]
        private ICanBeTalkTo m_talkingTo;
        [SerializeField]
        private bool m_isTalking = false;
        [SerializeField]
        private bool m_isUsingSkill = false;

        private CharacterController m_characterController;

        private IUseFSM m_playerFsm;
        public IUseFSM PlayerFsm
        {
            get { return this.m_playerFsm; }
        }

        /// <summary>
        /// 处于IsTalking状态时与玩家对话的NPC
        /// </summary>
        public ICanBeTalkTo IsTalkingTo
        {
            get { return m_talkingTo; }
        }

        public Player(GameObject player) : base(player)
        {
            this.m_playerFsm = new PlayerFsm(this);
            this.m_characterController = this.TargetGameObject.GetComponent<CharacterController>();
        }

        public static Player operator +(Player player, Equipment equipment)
        {
            if (equipment == null)
            {
                return player;
            }
            player.HP += equipment.HP;
            player.MagicAttack += equipment.MagicAttack;
            player.MagicDefense += equipment.MagicDefense;
            player.PhysicalAttack += equipment.PhysicalAttack;
            player.PhysicalDefense += equipment.PhysicalDefense;
            return player;
        }

        public static Player operator -(Player player, Equipment equipment)
        {
            if (equipment == null)
            {
                return player;
            }
            player.HP -= equipment.HP;
            player.MagicAttack -= equipment.MagicAttack;
            player.MagicDefense -= equipment.MagicDefense;
            player.PhysicalAttack -= equipment.PhysicalAttack;
            player.PhysicalDefense -= equipment.PhysicalDefense;
            return player;
        }

        public bool IsTalking
        {
            get { return m_isTalking; }
        }

        public void TalkTo(ICanBeTalkTo talker)
        {
            if (!this.m_isTalking && !talker.IsTalking)
            {
                talker.TalkTo(this);
                this.m_isTalking = true;
                this.m_talkingTo = talker;
            }
        }

        public void TalkFinish()
        {
            this.m_isTalking = false;
            this.m_talkingTo = null;
        }

        public bool IsUsingSkill
        {
            get { return m_isUsingSkill; }
        }

        public void UseSkill(SkillTree skill)
        {
        }

        public void StopSkill()
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.PlayerFsm.FixedUpdate();
            this.m_characterController.Move((Direction + Physics.gravity) * Time.fixedDeltaTime); //这里方向由PlayerFSM赋值
        }
    }
}
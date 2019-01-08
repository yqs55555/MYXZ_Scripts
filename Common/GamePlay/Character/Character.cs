/*
 * FileName             : Character.cs
 * Author               : yqs
 * Creat Date           : 2019.1.3
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MYXZ
{
    [ProtoContract]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public abstract class Character
    {
        [ProtoMember(1)]
        public int HP;

        [ProtoMember(2)]
        public int Level;

        [ProtoMember(3)]
        public int PhysicalAttack;

        [ProtoMember(4)]
        public int PhysicalDefense;

        [ProtoMember(5)]
        public int MagicAttack;

        [ProtoMember(6)]
        public int MagicDefense;

        [HideInInspector]
        public GameObject TargetGameObject;
        [HideInInspector]
        public Animator Animator;

        private CharacterController m_characterController;
        public float Mass;

        /// <summary>
        /// 随角色状态变动的运动方向
        /// </summary>
        public Vector3 Direction;
        public float BaseSpeed;
        public float JumpSpeed;
        [HideInInspector]
        public float Rate = 1.0f;
        public float WalkSpeed
        {
            get { return BaseSpeed * Rate; }
        }
        public float RunSpeed
        {
            get { return BaseSpeed * 2 * Rate; }
        }

        protected Character(GameObject gameObject)
        {
            TargetGameObject = gameObject;
            this.Animator = this.TargetGameObject.GetComponent<Animator>();
            this.m_characterController = this.TargetGameObject.GetComponent<CharacterController>();
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {
            this.m_characterController.Move((Direction + Physics.gravity) * Time.fixedDeltaTime);
        }
    }
}
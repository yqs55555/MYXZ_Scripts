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

using ProtoBuf;
using UnityEngine;

namespace MYXZ
{
    [ProtoContract]
    [RequireComponent(typeof(Animator))]
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

        private GameObject m_targetGameObject;
        public GameObject TargetGameObject
        {
            get { return this.m_targetGameObject; }
        }
        [HideInInspector]
        public Animator Animator;

        public float Mass;

        /// <summary>
        /// 随角色状态变动的运动方向
        /// </summary>
        public Vector3 Direction;
        [SerializeField]
        private float m_baseSpeed;

        public float BaseSpeed
        {
            get { return this.m_baseSpeed; }
            set { m_baseSpeed = value; }
        }
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
            this.m_targetGameObject = gameObject;
            this.Animator = this.TargetGameObject.GetComponent<Animator>();
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }
    }
}
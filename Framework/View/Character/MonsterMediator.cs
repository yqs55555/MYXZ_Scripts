/*
 * FileName             : MonsterMediator.cs
 * Author               : hy
 * Creat Date           : 2018.11.2
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
    /// Monster的Mediator
    /// </summary>
    public class MonsterMediator : Mediator
    {
        [Inject]
        public MonsterView View { get; set; }

        [Inject]
        public RegisterSkillSignal RegisterSkillSignal { get; set; }

        [Inject]
        public BeAttackedSignal BeAttackedSignal { get; set; }

        [Inject]
        public GetSkillInputSignal GetSkillInputSignal { get; set; }

        public override void OnRegister()
        {
            RegisterSkillSignal.Dispatch(this.gameObject, View.SkillTreeID);
            //View.Character.UseSkill = () => StartCoroutine(SkillUsing());
//            View.BeAttackedSignal.AddListener(BeAttacked);
//            View.UseSkillSignal.AddListener(UseSkill);
        }

        public override void OnRemove()
        {
//            View.BeAttackedSignal.RemoveListener(BeAttacked);
        }

        private void UseSkill(KeyCode input)
        {
            GetSkillInputSignal.Dispatch(this.gameObject, input);
        }

        private void BeAttacked(Transform attacker, SkillLeaf skill)
        {
            BeAttackedSignal.Dispatch(attacker.gameObject, this.transform, skill);
        }

//        public IEnumerator SkillUsing()
//        {
//            float timer = 0.0f;
//            this.View.Character.Rate = 0.0f;
//
//            while (timer < this.View.Character.CurrentSkill.SkillTime)
//            {
//                timer += Time.deltaTime;
//                yield return null;
//            }
//            this.View.Character.Rate = 1.0f;
//            this.View.Character.CurrentSkill.SkillExit();
//            this.View.Character.SetCurrentSkill(null);
//        }
    }
}
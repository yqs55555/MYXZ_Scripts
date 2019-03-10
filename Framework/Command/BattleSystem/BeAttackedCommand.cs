/*
 * FileName             : BeAttackedCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.11.5
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using strange.extensions.command.impl;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    public class BeAttackedCommand : Command
    {
        [Inject]
        public GameObject Attacker { get; set; }

        [Inject]
        public Transform BeAttacked { get; set; }

        [Inject]
        public SkillLeaf Skill { get; set; }

        public override void Execute()
        {
//            View beAttackedView = BeAttacked.GetComponent<View>();
//            int damage = Attacker.GetComponent<CharacterView>().Character.PhysicalAttack -
//                         BeAttacked.GetComponent<CharacterView>().Character.PhysicalDefense;
//            beAttackedView.Character.HP -= damage;
//            if (damage > 0)
//            {
//                bl_HUDText.Instance.SetHUDText("-" + damage.ToString(), BeAttacked, Color.red,
//                    bl_Guidance.LeftDown, 50, 1.0f);
//            }
//            else
//            {
//                bl_HUDText.Instance.SetHUDText("Miss",BeAttacked,Color.yellow, bl_Guidance.LeftDown, 50, 1.0f);
//            }
//
//            if (beAttackedView.Character.HP <= 0)
//            {
//                Object.Destroy(beAttackedView.GetComponent<BehaviorTree>());
//                beAttackedView.Character.Animator.Play("death");
//                Object.Destroy(beAttackedView.gameObject, 2.5f);
//            }
        }
    }
}
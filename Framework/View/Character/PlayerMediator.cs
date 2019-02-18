/*
 * FileName             : PlayerMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.impl;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 玩家的Mediator
    /// </summary>
    public class PlayerMediator : Mediator
    {
        [Inject]
        public PlayerView View { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        [Inject]
        public FinishTalkAndChooseSignal FinishTalkSignal { get; set; }

        [Inject]
        public ResponseGetCharaInfoSignal ResGetCharaInfoSignal { get; set; }

        [Inject]
        public ResponsePlayerTransformSignal ResGetPlayerTransformSignal { get; set; }

        [Inject]
        public RequestGetPlayerTransformSignal ReqGetPlayerTransformSignal { get; set; }

        [Inject]
        public ChangeSceneSignal ChangeSceneSignal { get; set; }

        [Inject]
        public RegisterSkillSignal RegisterSkillSignal { get; set; }

        [Inject]
        public GetSkillInputSignal GetSkillInputSignal { get; set; }

        [Inject]
        public BeAttackedSignal BeAttackedSignal { get; set; }

        [Inject]
        public InitPlayerSignal InitPlayerSignal { get; set; }

        public override void OnRegister()
        {
            this.View.Player = new Player(this.gameObject);
            InitPlayerSignal.Dispatch(this.View.Player);
            MYXZUIManager.Instance.PushPanel(UIPanelType.WorldSpaceBackGroundPanel);
            ResGetPlayerTransformSignal.AddListener(SetPlayerPosition);
            View.EscSignal.AddListener(KeyEscDown);
//            View.UseSkillSignal.AddListener(UseSkill);
//            View.BeAttackedSignal.AddListener(BeAttacked);
            FinishTalkSignal.AddListener(TalkFinish);
            ResGetCharaInfoSignal.AddListener(GetCharaInfo);
            RegisterSkillSignal.Dispatch(this.gameObject, View.SkillTreeID);

//            ReqGetPlayerTransformSignal.Dispatch();
            //            View.Character.UseSkill = () => StartCoroutine(SkillUsing());
//            ReqGetCharacterInfoSignal.Dispatch();
        }

        public override void OnRemove()
        {
            View.EscSignal.RemoveListener(KeyEscDown);
//            View.UseSkillSignal.RemoveListener(UseSkill);
//            View.BeAttackedSignal.RemoveListener(BeAttacked);
            FinishTalkSignal.RemoveListener(TalkFinish);
            ResGetCharaInfoSignal.RemoveListener(GetCharaInfo);
            ResGetPlayerTransformSignal.RemoveListener(SetPlayerPosition);

        }

        private void BeAttacked(Transform attacker, SkillBase skill)
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

        void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("TeleportationCircle"))
            {
                ChangeSceneSignal.Dispatch("Scene02");
            }
        }

        private void KeyEscDown()
        {
            PushPanelSignal.Dispatch(UIPanelType.SmallSettingBoxPanel);
        }

        /// <summary>
        /// 对话结束
        /// </summary>
        private void TalkFinish()
        {
            this.View.Player.IsTalkingTo.TalkFinish(); //与Player对话的对象离开对话状态
            this.View.Player.TalkFinish();
        }

        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="info"></param>
        private void GetCharaInfo(Player info)
        {
            this.View.Player = info;
        }

        /// <summary>
        /// 人物位置赋值
        /// </summary>
        private void SetPlayerPosition(SaveInfo.Transform transform)
        {
            View.transform.position = transform.Position;
            View.transform.rotation = Quaternion.Euler(transform.Rotation);
        }

        private void UseSkill(KeyCode input)
        {
            GetSkillInputSignal.Dispatch(this.gameObject, input);
        }
    }
}
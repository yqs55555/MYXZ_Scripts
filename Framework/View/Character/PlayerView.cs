/*
 * FileName             : PlayerView.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 玩家所见的View
    /// </summary>
    public class PlayerView : View
    {
        public Player Player;
        public Signal EscSignal = new Signal();
        public UniStormWeatherSystem_C WeatherSystem;
        [SerializeField]
        public Dictionary<KeyCode, string> SkillTreeID = new Dictionary<KeyCode, string>()
        {
            { KeyCode.Alpha1, "110001"}
        };

        protected override void Start()
        {
            base.Start();
            WeatherSystem.SetDate(12, 12, 2018);
            WeatherSystem.ChangeWeatherInstant(12);
        }

        void Update()
        {
            Player.Update();
            if (MYXZInput.GetKeyDown(KeyCode.Escape))
            {
                EscSignal.Dispatch();
            }

            if ((this.Player.PlayerFsm.CurrentStateID == StateID.IdleState || this.Player.PlayerFsm.CurrentStateID == StateID.WalkState)
                    && MYXZInput.GetMouseButtonDown(0))
            {
                TryToTalk();
            }
        }

        void FixedUpdate()
        {
            Player.FixedUpdate();
        }

        private void TryToTalk()
        {
            GameObject talker;
            if ((talker = ClickEvent.OnMouseClickTag(0, "NPC", this.Player.TalkDistance)) != null)
            {
                NpcView view = talker.GetComponent<NpcView>();
                if (view == null)
                {
                    Debug.LogError(talker.name + "不是NPC");
                }
                else
                {
                    this.Player.TalkTo(view.NPC);
                }
            }
        }
    }
}
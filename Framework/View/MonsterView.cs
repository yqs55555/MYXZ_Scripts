/*
 * FileName             : MonsterView.cs
 * Author               : yqs
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
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 怪物所见的View
    /// </summary>
    public class MonsterView : View
    {
        public Character Character;
        [SerializeField]
        public Dictionary<KeyCode, string> SkillTreeID = new Dictionary<KeyCode, string>()
        {
            { KeyCode.Alpha1, "110002"}
        };

        public List<GameObject> WayPoints;

        protected override void Start()
        {
            base.Start();
            Character = new Player(this.gameObject);
            bl_HUDText.Instance.SetHUDText("怪物", this.transform, Color.blue, bl_Guidance.Static,30);
        }

    }
}
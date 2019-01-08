/*
 * FileName             : WorldSpaceBackGroundPanelView.cs
 * Author               : yqs
 * Creat Date           : 2018.2.1
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 在地图上行走时的底部View
    /// </summary>
    public class WorldSpaceBackGroundPanelView : BasePanelView
    {
        public Text MapName;
        public Signal OpenBagSignal = new Signal();
        private static Dictionary<string, string> mMapNameDic = new Dictionary<string, string>()
        {
            {"Scene01", "雾隐山 天青观"},
            {"Scene02", "苗疆 凤鸣镇"}
        };

        public override void OnEnter()
        {
            base.OnEnter();
            MapName.text = mMapNameDic[SceneManager.GetActiveScene().name];
        }

        public void OpenBagPanel()
        {
            OpenBagSignal.Dispatch();
        }

        public override void OnPause()
        {
            base.OnPause();
            MYXZInputManager.Instance.IsEnable = false;
        }

        public override void OnResume()
        {
            base.OnResume();
            MYXZInputManager.Instance.IsEnable = true;
        }
    }
}
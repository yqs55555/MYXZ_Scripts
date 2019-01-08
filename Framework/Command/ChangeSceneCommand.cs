/*
 * FileName             : ChangeSceneCommand.cs
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
using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MYXZ
{
    /// <summary>
    /// 跳转场景， Bind From ChangeSceneSignal
    /// </summary>
    public class ChangeSceneCommand : Command
    {
        [Inject]
        public string TargetScene { get; set; }

        [Inject]
        public TaskService TaskService { get; set; }

        private static Dictionary<string, string> mMessage = new Dictionary<string, string>()
        {
            {"Scene01", "确定前往雾隐山 天青观？"},
            {"StartScene", "返回开始界面？"},
            {"Scene02", "确定前往苗疆 凤鸣镇？"}
        };

        private static Dictionary<string, string> mSceneId = new Dictionary<string, string>()
        {
            {"Scene01", "060001"},
            {"Scene02", "060002"}
        };

        public override void Execute()
        {
            Debug.Log("Command");
            MessageBoxPanelView MessageBoxView =
                MYXZUIManager.Instance.GetPanel(UIPanelType.MessageBoxPanel) as MessageBoxPanelView;
            MessageBoxView.MessageText.text = mMessage[TargetScene];
            MessageBoxView.ConfirmEvent = ChangeScene;
            MessageBoxView.CancelEvent = MYXZUIManager.Instance.PopPanel;
            MYXZUIManager.Instance.PushPanel(UIPanelType.MessageBoxPanel);
        }

        private void ChangeScene()
        {
            if (!TargetScene.Equals("StartScene"))
            {
                TaskService.CheckTaskFinishOfScene(mSceneId[TargetScene]);
            }
            else
            {
                Object.Destroy(WorldSpaceGameRoot.Instance.gameObject);
            }
            MYXZUIManager.Instance.PopPanel();
            LoadingScenePanelView LoadingSceneView =
                MYXZUIManager.Instance.GetPanel(UIPanelType.LoadingScenePanel) as LoadingScenePanelView;
            LoadingSceneView.TargetSceneName = TargetScene;
            MYXZUIManager.Instance.PushPanel(UIPanelType.LoadingScenePanel);
            if (bl_HUDText.Instance != null)
            {
                bl_HUDText.Instance.Clear();
            }
        }
    }
}
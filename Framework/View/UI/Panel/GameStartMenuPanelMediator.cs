﻿/*
 * FileName             : GameStartMenuPanelMediator.cs
 * Author               : yqs
 * Creat Date           : 2017.12.28
 */

using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 开始菜单Mediator
    /// </summary>
    public class GameStartMenuPanelMediator : Mediator
    {
        [Inject]
        public GameStartMenuPanelView StartMenuView { get; set; }

        [Inject]
        public ChangeSceneSignal ChangeSceneSignal { get; set; }

        [Inject]
        public ExitGameSignal ExitGameSignal { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        public override void OnRegister()
        {
            StartMenuView.NewGame.AddListener(OnClickCreatNewGame);
            StartMenuView.ExitGame.AddListener(OnClickExitGame);
            StartMenuView.GameInfo.AddListener(OnClickGameInfo);
            StartMenuView.GameSave.AddListener(OnClickGameSave);
        }

        public override void OnRemove()
        {
            StartMenuView.NewGame.RemoveListener(OnClickCreatNewGame);
            StartMenuView.ExitGame.RemoveListener(OnClickExitGame);
            StartMenuView.GameInfo.RemoveListener(OnClickGameInfo);
            StartMenuView.GameSave.RemoveListener(OnClickGameSave);
        }

        private void OnClickCreatNewGame()
        {
            ChangeSceneSignal.Dispatch("Scene01");
        }

        private void OnClickExitGame()
        {
            ExitGameSignal.Dispatch();
        }

        private void OnClickGameInfo()
        {
            PushPanelSignal.Dispatch(UIPanelType.GameInfoPanel);
        }

        private void OnClickGameSave()
        {
            PushPanelSignal.Dispatch(UIPanelType.GameSavePanel);
        }
    }
}
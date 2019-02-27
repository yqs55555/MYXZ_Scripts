/*
 * FileName             : InitPlayerCommand.cs
 * Author               : yqs
 * Creat Date           : 2019.2.13
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 创建一个Player
    /// </summary>
    public class InitPlayerCommand : Command
    {
        [Inject]
        public Player Player { get; set; }

        [Inject]
        public PlayerInfoModel PlayerModel { get; set; }

        [Inject]
        public AOIInfoModel AOIModel { get; set; }

        [Inject]
        public SceneModel MapModel { get; set; }

        public override void Execute()
        {
            Player.BaseSpeed = 5;
            Player.TalkDistance = 5;

            PlayerModel.CurrentPlayer = Player;

            MYXZEntity entity = new MYXZEntity(Player, Setting.AOI.PLAYER_INTEREST_RADIUS);
            entity.RegisterObserver(AOIModel.Update);       //当Entity的AOIPosition发生变化时会通知AOIModel

            entity.UpdatePosition();    //初始化调用一次
            int mark = MYXZTimer.Instance.AddTimer(entity.UpdatePosition, Setting.AOI.UPDATE_RATE, -1);    //每隔一段时间更新一次坐标信息
            AOIModel.AddEntityUpdateMark(entity, mark);
            MapModel.AddEntity(Player.TargetGameObject, entity);
        }
    }
}
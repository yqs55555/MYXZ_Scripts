/*
 * FileName             : InitNpcCommand.cs
 * Author               : yqs
 * Creat Date           : 2019.2.13
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

namespace MYXZ
{
    public class InitNpcCommand : Command
    {
        [Inject]
        public NPC Npc { get; set; }

        [Inject]
        public string NpcId { get; set; }

        [Inject]
        public AOIInfoModel AOIInfoModel { get; set; }

        [Inject]
        public MapModel MapModel { get; set; }

        public override void Execute()
        {
            MYXZEntity entity = new MYXZEntity(Npc, 2);
            entity.RegisterObserver(AOIInfoModel.Update);

            entity.UpdatePosition();
            int mark = MYXZTimer.Instance.AddTimer(entity.UpdatePosition, Setting.AOI.UPDATE_RATE, -1);
            AOIInfoModel.AddEntityUpdateMark(entity, mark);
            MapModel.AddEntity(Npc.TargetGameObject, entity);
        }
    }
}
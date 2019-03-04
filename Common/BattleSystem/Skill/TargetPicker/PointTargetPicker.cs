/*
 * FileName             : PointTargetPicker.cs
 * Author               : yqs
 * Creat Date           : 2018.9.27
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 攻击区域
    /// </summary>
    public class PointTargetPicker : TargetPicker
    {
        private float mMinDistance;
        private float mMaxDistance;
        private float mViewAngle;

        public PointTargetPicker(float minDistance, float maxDistance, float viewAngle)
        {
            this.mMinDistance = minDistance;
            this.mMaxDistance = maxDistance;
            this.mViewAngle = viewAngle;
        }
        
        public override LinkedList<MYXZEntity> Pick(Transform player, IEnumerable<MYXZEntity> targets)
        {
            if (targets == null)
                return null;
            this.Targets.Clear();
            foreach (MYXZEntity entity in targets)
            {
                if (MYXZMath.IsInPointArea(player.position, player.forward, entity.Transform.position, mMinDistance, mMaxDistance,
                    mViewAngle))
                {
                    this.Targets.AddFirst(entity);
                }
            }
            return this.Targets;
        }
    }
}
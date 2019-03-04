/*
 * FileName             : RectTargetPicker.cs
 * Author               : 
 * Creat Date           : 
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
    public class RectTargetPicker : TargetPicker
    {
        private float m_width;
        private float m_length;

        public RectTargetPicker(float width, float length)
        {
            this.m_width = width;
            this.m_length = length;
        }

        public override LinkedList<MYXZEntity> Pick(Transform player, IEnumerable<MYXZEntity> targets)
        {
            if (targets == null)
                return null;
            this.Targets.Clear();
            foreach (MYXZEntity entity in targets)
            {
                if (MYXZMath.IsInRectArea(player.position, player.forward, entity.Transform.position, this.m_width, this.m_length))
                {
                    this.Targets.AddFirst(entity);
                }
            }
            return this.Targets;
        }
    }
}
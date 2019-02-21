/*
 * FileName             : UITypeCompare.cs
 * Author               : yqs
 * Creat Date           : 2019.2.21
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
    /// 比较两个UIPanelType，用于解决UIPanelType作为Dictionary的Key时带来的GC
    /// </summary>
    public class UITypeCompare : IEqualityComparer<UIPanelType>
    {
        public bool Equals(UIPanelType x, UIPanelType y)
        {
            return (int)x == (int)y;
        }

        public int GetHashCode(UIPanelType obj)
        {
            return (int)obj;
        }
    }
}
/*
 * FileName             : WorldSpaceGameRoot.cs
 * Author               : yqs
 * Creat Date           : 2018.1.30
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using strange.extensions.context.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 世界场景的GameRoot，绑定在对应的物体上
    /// </summary>
    public class WorldSpaceGameRoot : ContextView
    {
        public static WorldSpaceGameRoot Instance;
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (Instance == null)
            {
                Instance = this;
                this.context = new WorldSpaceContext(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}

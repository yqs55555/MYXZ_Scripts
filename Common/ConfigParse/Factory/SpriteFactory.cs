/*
 * FileName             : SpriteFactory.cs
 * Author               : yqs
 * Creat Date           : 2019.2.25
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
    public class SpriteFactory : IConfigFactory<Sprite>
    {
        private AssetBundle m_assetBundle;

        public SpriteFactory(AssetBundle assetBundle)
        {
            this.m_assetBundle = assetBundle;
        }

        public Sprite Create(string id)
        {
            return m_assetBundle.LoadAsset<Sprite>(id);
        }

        object IConfigFactory.Create(string id)
        {
            return Create(id);
        }
    }
}
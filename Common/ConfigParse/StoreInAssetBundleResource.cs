/*
 * FileName             : StoreInAssetBundleResource.cs
 * Author               : yqs
 * Creat Date           : 2019.2.20
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 存储在AssetBundle中的资源的相关信息
    /// </summary>
    public class StoreInAssetBundleResource : IStoreInAssetBundle
    {
        /// <summary>
        /// 资源的类型
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// 资源是否是分块存储在AssetBundle中的
        /// </summary>
        public bool IsChunked { get; private set; }
        /// <summary>
        /// 分块的大小
        /// </summary>
        private int m_chunk;
        /// <summary>
        /// 基本路径
        /// </summary>
        private string m_baseAssetBundle;

        public StoreInAssetBundleResource(string type, string assetBundlePath) : this(type, assetBundlePath, -1)
        {
        }

        public StoreInAssetBundleResource(string type, string assetBundlePath, int chunk)
        {
            this.Type = type;
            this.m_baseAssetBundle = assetBundlePath;
            if (chunk > 0)
            {
                this.m_chunk = chunk;
                this.IsChunked = true;
            }
            else
            {
                this.IsChunked = false;
            }
        }

        public string GetAssetBundlePath()
        {
            return GetAssetBundlePath(string.Empty);
        }

        /// <summary>
        /// 某个id的资源存储的AssetBundle路径
        /// </summary>
        /// <param name="idWithoutType"></param>
        /// <returns>具体的路径</returns>
        public string GetAssetBundlePath(string idWithoutType)
        {
            if (this.IsChunked  && !idWithoutType.Equals(string.Empty))
            {
                int count = Int32.Parse(idWithoutType);
                string index = (count / this.m_chunk).ToString();
                return string.Concat(this.m_baseAssetBundle, index);
            }
            return this.m_baseAssetBundle;
        }
    }
}
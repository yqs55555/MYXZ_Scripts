/*
 * FileName             : IStoreInAssetBundle.cs
 * Author               : yqs
 * Creat Date           : 2019.2.20
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
    public interface IStoreInAssetBundle
    {
        string Type { get; }
        string GetAssetBundlePath();
        string GetAssetBundlePath(string idWithoutType);
        bool IsChunked { get; }
    }
}
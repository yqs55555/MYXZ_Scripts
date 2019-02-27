/*
 * FileName             : MYXZXmlReader.cs
 * Author               : ZSZ
 * Creat Date           : 2018.2.12
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace MYXZ
{
    /// <summary>     
    /// XML文件读取  
    /// </summary>
    public class MYXZXmlReader
    {
        /// <summary>
        /// 获取目标id的配置文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetConfigAssetBundlePath(string id)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(new StreamReader(
#if UNITY_EDITOR
                System.IO.Path.Combine(Application.dataPath,
                    System.IO.Path.Combine("ABResources/Config", "TotalConfig.xml"))
#else
                System.IO.Path.Combine(Application.dataPath, Setting.Config.ROOT)
#endif
                ).ReadToEnd());
            XmlNode rootNode = doc.FirstChild;
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Attributes["Id"].Value.Equals(id))
                {
                    return node.Attributes["Path"].Value;
                }
            }
            Debug.Log("未找到Id为" + id + "配置文件");
            return "";
        }
    }
}
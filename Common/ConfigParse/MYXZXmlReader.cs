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

        /// <summary>
        /// 根据技能树的ID来获取一个技能树实例
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns>技能树的根节点</returns>
        public static SkillRootNode ReadSkillTree(string skillId)
        {

        }

        /// <summary>
        /// 从xmlFile中读取此SceneInfo的信息
        /// </summary>
        /// <param name="sceneInfo">读取得到的信息out赋值给sceneInfo</param>
        /// <param name="xmlText">xml的string</param>
        /// <returns>是否读取成功</returns>
        public static bool ReadSceneInfo(out SceneInfo sceneInfo, string xmlText)
        {
            sceneInfo = new SceneInfo();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlText); //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return false;
            }

            XmlNode sceneInfoNode = doc.FirstChild; //获取SceneInfo节点
            sceneInfo.Name = sceneInfoNode.Attributes["Name"].Value; //获取当前场景的名称
            sceneInfo.Id = sceneInfoNode.Attributes["Id"].Value; //获取当前场景的Id

            foreach (XmlNode node in sceneInfoNode.ChildNodes)
            {
                if (node.Name.Equals("NPC"))
                {
                    sceneInfo.Npcs = node.Attributes["Id"].Value.Split(','); //获取当前场景所有NPC的ID
                }
            }
            return true;
        }


    }
}
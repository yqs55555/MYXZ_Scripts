/*
 * FileName             : SceneInfoFactory.cs
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
using System.Xml;
using UnityEngine;

namespace MYXZ
{
    public class SceneInfoFactory : IConfigFactory<SceneInfo>
    {
        private AssetBundle m_assetBundle;

        public SceneInfoFactory(AssetBundle assetBundle)
        {
            this.m_assetBundle = assetBundle;
        }

        public SceneInfo Create(string id)
        {
            TextAsset sceneInfoText = this.m_assetBundle.LoadAsset<TextAsset>(id);
            if (sceneInfoText == null)
            {
                Debug.LogError("无法找到id为" + id + "的Scene的信息");
                return null;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(sceneInfoText.text); //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }

            SceneInfo sceneInfo = new SceneInfo();
            XmlNode sceneInfoNode = doc.FirstChild; //获取SceneInfo节点
            sceneInfo.Name = sceneInfoNode.Attributes["Name"].Value; //获取当前场景的名称
            sceneInfo.Id = sceneInfoNode.Attributes["Id"].Value; //获取当前场景的Id

            foreach (XmlNode node in sceneInfoNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "MapSize":
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            switch (childNode.Name)
                            {
                                case "Begin":
                                    string[] pos = childNode.InnerText.Split(',');
                                    sceneInfo.MapBegin = new Vector2Int(
                                        int.Parse(pos[0]), int.Parse(pos[1])
                                        );
                                    break;
                                case "End":
                                    pos = childNode.InnerText.Split(',');
                                    sceneInfo.MapEnd = new Vector2Int(
                                        int.Parse(pos[0]), int.Parse(pos[1])
                                        );
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }

            return sceneInfo;
        }

        object IConfigFactory.Create(string id)
        {
            return Create(id);
        }
    }
}
/*
 * FileName             : Setting.cs
 * Author               : yqs
 * Creat Date           : 2019.2.17
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
    public class Setting
    {
        private const string ROOT_CONFIG_PATH = "Setting.xml";
        public static readonly string BASE_PATH =
#if UNITY_EDITOR
            Path.Combine(Application.dataPath, "ABResources/Config");
#else
            Application.dataPath;
#endif
        public static readonly string BASE_ASSET_BUNDLE_PATH = Application.streamingAssetsPath;
        private static bool m_hasInit = false;

        #region InitMembers
        public static void Init()
        {
            if (!m_hasInit)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(new StreamReader(Path.Combine(BASE_PATH, ROOT_CONFIG_PATH)).ReadToEnd());

                XmlNode setting = xml.FirstChild;
                foreach (XmlNode node in setting.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Type":
                            InitConfig(node);
                            break;
                        case "AssetBundlePath":
                            InitAssetBundlePath(node);
                            break;
                        case "Save":
                            InitSave(node);
                            break;
                        case "AOI":
                            InitAOI(node);
                            break;
                        default:
                            break;
                    }
                }
                m_hasInit = true;
            }
        }

        private static void InitConfig(XmlNode configNode)
        {
            foreach (XmlNode childNode in configNode.ChildNodes)
            {
                Config.Id2Type.Add(
                    childNode.Attributes["Id"].Value,
                    childNode.Attributes["Type"].Value
                    );
            }
        }

        private static void InitAssetBundlePath(XmlNode pathNode)
        {
            foreach (XmlNode childNode in pathNode.ChildNodes)
            {
                IStoreInAssetBundle storeInAssetBundleRes;
                string type = childNode.Attributes["Type"].Value;
                string path = childNode.Attributes["Path"].Value;
                int chunk = -1;
                foreach (XmlNode childNodeChildNode in childNode.ChildNodes)
                {
                    switch (childNodeChildNode.Name)
                    {
                        case "Chunk":       //如果存在分块这个属性
                            chunk = Int32.Parse(childNodeChildNode.InnerText);
                            break;
                    }
                }
                storeInAssetBundleRes = new StoreInAssetBundleResource(type, path, chunk);
                AssetBundlePath.Type2StoreInfo.Add(type, storeInAssetBundleRes);
            }
        }

        private static void InitSave(XmlNode saveNode)
        {
            foreach (XmlNode childNode in saveNode.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderPath":
                        Save.FolderPath = childNode.InnerText;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void InitAOI(XmlNode aoiNode)
        {
            foreach (XmlNode childNode in aoiNode.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "GridField":
                        AOI.GRID_FIELD = int.Parse(childNode.InnerText);
                        break;
                    case "UpdateRate":
                        AOI.UPDATE_RATE = float.Parse(childNode.InnerText);
                        break;
                    case "PlayerInterestRadius":
                        AOI.PLAYER_INTEREST_RADIUS = int.Parse(childNode.InnerText);
                        break;
                    case "NPCInterstRadius":
                        AOI.NPC_INTEREST_RADIUS = int.Parse(childNode.InnerText);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        public class Config
        {
            public static Dictionary<string, string> Id2Type = new Dictionary<string, string>();

            /// <summary>
            /// 获取目标id的类型
            /// </summary>
            /// <param name="id">id</param>
            /// <returns>id的类型</returns>
            public static string GetTypeId(string id)
            {
                return id.Substring(0, 2);
            }

            /// <summary>
            /// 获取目标id在同类中的索引
            /// </summary>
            /// <param name="id">id</param>
            /// <returns>id的索引</returns>
            public static string GetIndex(string id)
            {
                return id.Substring(2);
            }
        }

        public class AssetBundlePath
        {
            public static Dictionary<string, IStoreInAssetBundle> Type2StoreInfo = new Dictionary<string, IStoreInAssetBundle>();
        }

        public class Save
        {
            public static string FolderPath;
        }

        public class AOI
        {
            public static float GRID_FIELD = 20;
            public static float UPDATE_RATE = 0.2f;
            public static int PLAYER_INTEREST_RADIUS = 3;
            public static int NPC_INTEREST_RADIUS = 2;
        }
    }
}
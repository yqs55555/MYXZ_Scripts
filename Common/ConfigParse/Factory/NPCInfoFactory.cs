/*
 * FileName             : NPCInfoFactory.cs
 * Author               : yqs
 * Creat Date           : 2019.2.21
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System.Xml;
using UnityEngine;

namespace MYXZ
{
    public class NPCInfoFactory : IConfigFactory<NpcInfo>
    {
        private AssetBundle m_assetBundle;

        public NPCInfoFactory(AssetBundle assetBundle)
        {
            this.m_assetBundle = assetBundle;
        }

        public NpcInfo Create(string id)
        {
            TextAsset npcText = m_assetBundle.LoadAsset<TextAsset>(id);
            if (npcText == null)
            {
                return null;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(npcText.text); //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }
            NpcInfo npcInfo = new NpcInfo();
            XmlNode npcNode = doc.FirstChild;                   //取得此NPC节点

            npcInfo.Name = npcNode.Attributes["Name"].Value;    //此NPC的名字
            npcInfo.Id = npcNode.Attributes["Id"].Value;        //此NPC的ID
            string spriteId = npcNode.Attributes["Sprite"].Value;
            npcInfo.Sprite = MYXZConfigLoader.Instance.GetSprite(spriteId);

            foreach (XmlNode node in npcNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Task":
                        npcInfo.TaskIds = node.Attributes["Id"].Value.Split(',');       //获取此NPC的任务
                        break;
                    case "Talk":
                        npcInfo.Talks = ConfigFactoryHelper.GetTalks(node);                                 //获取此NPC的交谈
                        break;
                    default:
                        break;
                }
            }

            return npcInfo;
        }

        object IConfigFactory.Create(string id)
        {
            return Create(id);
        }
    }
}
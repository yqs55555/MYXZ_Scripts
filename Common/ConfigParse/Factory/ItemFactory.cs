/*
 * FileName             : EquipmentFactory.cs
 * Author               : yqs
 * Creat Date           : 2019.2.19
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Xml;
using UnityEngine;

namespace MYXZ
{
    public class ItemFactory : IConfigFactory<Item>
    {
        private AssetBundle m_assetBundle;

        public ItemFactory(AssetBundle assetBundle)
        {
            this.m_assetBundle = assetBundle;
        }

        public Item Create(string id)
        {
            TextAsset itemText = m_assetBundle.LoadAsset<TextAsset>(id);
            if (itemText == null)
            {
                Debug.LogError("无法找到id为" + id + "的Item的信息");
                return null;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(itemText.text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }
            XmlNode itemNode = doc.FirstChild;

            string name = itemNode.Attributes["Name"].Value;
            string spriteId = itemNode.Attributes["Sprite"].Value;
            string itemDescription = "";
            string useDescription = "";
            int buyPrice = 0;
            int salePrice = 0;

            int hp = 0;
            int physicalAttack = 0;
            int physicalDefense = 0;
            int magicAttack = 0;
            int magicDefense = 0;
            Equipment.Type equipmentType = Equipment.Type.Weapon;

            foreach (XmlNode node in itemNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "ItemDescription":
                        itemDescription = node.InnerText;
                        break;
                    case "UseDescription":
                        useDescription = node.InnerText;
                        break;
                    case "SalePrice":
                        salePrice = int.Parse(node.InnerText);
                        break;
                    case "BuyPrice":
                        buyPrice = Int32.Parse(node.InnerText);
                        break;
                    case "HP":
                        hp = Int32.Parse(node.InnerText);
                        break;
                    case "PhysicalAttack":
                        physicalAttack = Int32.Parse(node.InnerText);
                        break;
                    case "MagicAttack":
                        magicAttack = Int32.Parse(node.InnerText);
                        break;
                    case "PhysicalDefense":
                        physicalDefense = Int32.Parse(node.InnerText);
                        break;
                    case "MagicDefense":
                        magicDefense = Int32.Parse(node.InnerText);
                        break;
                    case "Type":
                        equipmentType = (Equipment.Type)Enum.Parse(typeof(Equipment.Type), node.InnerText);
                        break;
                    default:
                        break;
                }
            }

            string itemType = Setting.Config.Id2Type[id.Substring(0, 2)];
            switch (itemType)
            {
                case "Consumable":
                    return new Consumable(name, id, spriteId, 
                        itemDescription, useDescription, buyPrice, 
                        salePrice, hp);
                case "Equipment":
                    return new Equipment(name, id, spriteId, 
                        itemDescription, useDescription, buyPrice, 
                        salePrice, physicalAttack, magicAttack, 
                        physicalDefense, magicDefense, hp, 
                        equipmentType);
                case "TaskItem":
                    return new TaskItem(name, id, spriteId, 
                        itemDescription, useDescription);
                case "Material":
                    return new Material(name, id, spriteId, 
                        itemDescription, useDescription, buyPrice, 
                        salePrice);
                default:
                    break;
            }

            return null;
        }

        object IConfigFactory.Create(string id)
        {
            return Create(id);
        }
    }
}
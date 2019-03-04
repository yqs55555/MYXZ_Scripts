/*
 * FileName             : SkillFactory.cs
 * Author               : yqs
 * Creat Date           : 2019.2.21
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
    public class SkillFactory : IConfigFactory<SkillRootNode>
    {
        private AssetBundle m_assetBundle;

        public SkillFactory(AssetBundle assetBundle)
        {
            this.m_assetBundle = assetBundle;
        }

        public SkillRootNode Create(string id)
        {
            TextAsset skillTreeText = this.m_assetBundle.LoadAsset<TextAsset>(id);

            SkillRootNode rootNode = new SkillRootNode(id);
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(skillTreeText.text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }

            XmlNode skillTreeNode = doc.FirstChild;
            rootNode.Loop = bool.Parse(skillTreeNode.Attributes["Loop"].Value);
            rootNode.Name = skillTreeNode.Attributes["Name"].Value;
            TraverseToGetSkillTree(rootNode, skillTreeNode);

            return rootNode;
        }

        object IConfigFactory.Create(string id)
        {
            return Create(id);
        }

        /// <summary>
        /// 递归来获取整棵技能树
        /// </summary>
        /// <param name="skillNode"></param>
        /// <param name="xmlNode"></param>
        private void TraverseToGetSkillTree(SkillNode skillNode, XmlNode xmlNode)
        {
            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    if (childNode.Name.Equals("Condition")) //条件节点
                    {
                        SkillCondition skillCondition = new SkillCondition("Condition");
                        skillNode.AddChildNode(skillCondition);
                        TraverseToGetSkillTree(skillCondition, childNode);
                    }

                    if (childNode.Name.Equals("SkillBase")) //具体技能
                    {
                        SkillBase skillBase = GetSkillBase(childNode.Attributes["Id"].Value);
                        skillNode.AddChildNode(skillBase);
                        TraverseToGetSkillTree(skillBase, childNode);
                    }
                }
            }
        }

        /// <summary>
        /// 返回此节点指向的子技能对应的SkillBase
        /// </summary>
        /// <param name="skillBaseId"></param>
        /// <returns></returns>
        private SkillBase GetSkillBase(string skillBaseId)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(m_assetBundle.LoadAsset<TextAsset>(skillBaseId).text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }

            TargetPicker attackArea = null;
            string animationName = "";
            float skillTime = 0.0f;
            foreach (XmlNode childNode in doc.FirstChild.ChildNodes)
            {
                if (childNode.Name.Equals("AttackArea"))
                {
                    switch (childNode.Attributes["Mode"].Value)
                    {
                        case "Point":
                            attackArea = new PointTargetPicker(
                                float.Parse(childNode.Attributes["MinDistance"].Value),
                                float.Parse(childNode.Attributes["MaxDistance"].Value),
                                float.Parse(childNode.Attributes["Angle"].Value));
                            break;
                        case "Rect":
                            attackArea = new RectTargetPicker(
                                float.Parse(childNode.Attributes["Width"].Value),
                                float.Parse(childNode.Attributes["Length"].Value));
                            break;
                    }
                }

                if (childNode.Name.Equals("Animation"))
                {
                    animationName = childNode.Attributes["Name"].Value;
                }

                if (childNode.Name.Equals("SkillTime"))
                {
                    skillTime = float.Parse(childNode.InnerText);
                }
            }
            return new SkillBase(skillBaseId, attackArea, animationName, skillTime);
        }
    }
}
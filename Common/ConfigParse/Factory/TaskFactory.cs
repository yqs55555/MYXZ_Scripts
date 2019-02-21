/*
 * FileName             : TaskFactory.cs
 * Author               : 
 * Creat Date           : 
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace MYXZ
{
    public class TaskFactory : IConfigFactory<Task>
    {
        private AssetBundle m_assetBundle;

        public TaskFactory(AssetBundle assetBundle)
        {
            this.m_assetBundle = assetBundle;
        }

        public Task Create(string id)
        {
            TextAsset taskText = m_assetBundle.LoadAsset<TextAsset>(id);
            if (taskText == null)
            {
                Debug.LogError("无法找到id为" + id + "的Task的信息");
                return null;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(taskText.text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }

            XmlNode taskNode = doc.FirstChild;

            Task task = new Task();

            task.Name = taskNode.Attributes["Name"].Value;              //任务名字
            task.Id = taskNode.Attributes["Id"].Value;                  //任务ID
            task.Publisher = taskNode.Attributes["Publisher"].Value;    //任务发布者ID
            task.Deliverer = taskNode.Attributes["Deliverer"].Value;    //任务交付者ID

            foreach (XmlNode node in taskNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Require":
                        if (!node.Attributes["Predecessors"].Value.Equals(""))    //如果有前置任务
                        {
                            task.Predecessors = ConfigFactoryHelper.GetIds(node.Attributes["Predecessors"].Value); //设置前置任务
                        }
                        task.LevelRequirement = Int32.Parse(node.Attributes["Level"].Value);      //等级需求
                        if (!node.Attributes["Item"].Value.Equals(""))            //如果有道具需求
                        {
                            task.RequireItems = ConfigFactoryHelper.GetIdsWithCount(
                                node.Attributes["Item"].Value   //接取任务所需要的物品，item[0]是ID，item[1]是数量
                                );
                        }
                        break;
                    case "Description":
                        task.Description = node.InnerText;            //对任务的描述
                        break;
                    case "Target":
                        task.Targets = new Task.Target[node.ChildNodes.Count];
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            task.Targets[i] = new Task.Target();
                            task.Targets[i].Type = (TaskTargetType)Enum.Parse(typeof(TaskTargetType), node.ChildNodes[i].Attributes["Type"].Value);//任务类型
                            task.Targets[i].TargetId = node.ChildNodes[i].Attributes["Id"].Value;                                              //任务目标ID
                            task.Targets[i].Count = int.Parse(node.ChildNodes[i].Attributes["Count"].Value);        //对目标操作次数
                        }
                        break;
                    case "TakeTask":
                        task.TakeTaskTalk = ConfigFactoryHelper.GetTalks(node);     //接取任务时的对话
                        break;
                    case "InTask":
                        task.InTaskTalk = ConfigFactoryHelper.GetTalks(node);       //在任务中的对话
                        break;
                    case "FinishTask":
                        task.FinishTaskTalk = ConfigFactoryHelper.GetTalks(node);   //任务交付时的对话，只有交付者不是010000（系统）时才有
                        break;
                    case "TakeTaskReward":
                        task.TakeTaskExperience = int.Parse(node.Attributes["Experience"].Value);
                        ConfigFactoryHelper.ReadRewardMoney(node.Attributes["Money"].Value, ref task.TakeTaskGold, ref task.TakeTaskSilver, ref task.TakeTaskCopper);
                        task.TakeTaskItem = ConfigFactoryHelper.GetIdsWithCount(node.InnerText);
                        break;
                    case "FinishTaskReward":
                        task.FinishTaskExperience = int.Parse(node.Attributes["Experience"].Value);
                        ConfigFactoryHelper.ReadRewardMoney(node.Attributes["Money"].Value, ref task.FinishTaskGold, ref task.FinishTaskSilver, ref task.FinishTaskCopper);
                        task.FinishTaskItem = ConfigFactoryHelper.GetIdsWithCount(node.InnerText);
                        break;
                    default:
                        break;
                }
            }

            return task;
        }

        object IConfigFactory.Create(string id)
        {
            return Create(id);
        }
    }
}
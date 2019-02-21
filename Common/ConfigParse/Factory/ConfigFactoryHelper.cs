/*
 * FileName             : ConfigFactoryHelper.cs
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
    public class ConfigFactoryHelper
    {
        public static Talk[] GetTalks(XmlNode node)
        {
            Talk[] talks = new Talk[node.ChildNodes.Count];
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                talks[i] = new Talk
                {
                    Speaker = node.ChildNodes[i].Attributes["Speaker"].Value,   //这句话的讲诉者
                    Words = node.ChildNodes[i].InnerText                        //这句话的内容
                };
            }
            return talks;
        }

        /// <summary>
        /// 从content中解析得到id和count
        /// </summary>
        /// <param name="content">未解析的内容</param>
        public static Dictionary<string, int> GetIdsWithCount(string content)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            string[] items = content.Split(',');
            foreach (string item in items)
            {
                string[] detail = item.Split('/');
                dictionary.Add(detail[0], int.Parse(detail[1]));
            }
            return dictionary;
        }

        /// <summary>
        /// 从xmlFile中读取此ReadRewardMoney的信息
        /// </summary>
        /// <param name="money">xml中被读取的金钱</param>
        /// <param name="gold">奖励的金币</param>
        /// <param name="silver">奖励的银币</param>
        /// <param name="copper">奖励的铜币</param>
        public static void ReadRewardMoney(string money, ref int gold, ref int silver, ref int copper)
        {
            int temp = 0;
            string tmpMoney = "";
            for (int i = 0; i < money.Length; i++)
            {
                if (money[i] != '/')
                {
                    tmpMoney = tmpMoney + money[i];
                }
                else if (money[i] == '/' && temp == 0)
                {
                    gold = int.Parse(tmpMoney);
                    tmpMoney = "";
                    temp++;
                }
                else if (money[i] == '/' && temp == 1)
                {
                    silver = int.Parse(tmpMoney);
                    tmpMoney = "";
                    temp++;
                }
                if (i + 1 == money.Length && temp == 2)
                {
                    copper = int.Parse(tmpMoney);
                    tmpMoney = "";
                    temp++;
                }
            }
        }

        public static string[] GetIds(string content)
        {
            return content.Split(',');
        }
    }
}
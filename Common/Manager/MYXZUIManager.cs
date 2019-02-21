﻿/*
 * FileName             : MYXZUIManager.cs
 * Author               : yqs
 * Creat Date           : 2018.9.19
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
    /// <summary>
    /// UI管理
    /// </summary>
    public class MYXZUIManager
    {
        #region Singleton
        public static MYXZUIManager Instance
        {
            get { return mInstance = mInstance ?? new MYXZUIManager(); }
        }

        private static MYXZUIManager mInstance;

        private MYXZUIManager()
        {
            Init();
        }
        #endregion

        private Dictionary<UIPanelType, BasePanelView> m_type2Panel;
        private Dictionary<UIPanelType, UIPanelInfo> m_type2Info;
        public Stack<BasePanelView> UIPanelStack;

        private void Init()
        {
            this.m_type2Info = MYXZConfigLoader.Instance.GetUIConfig();
            this.m_type2Panel = new Dictionary<UIPanelType, BasePanelView>(new UITypeCompare());
        }

        /// <summary>
        /// 获取UIPanelView
        /// </summary>
        /// <param name="type">要获取的UIPanelView的type</param>
        /// <returns>目标UIPanelView</returns>
        public BasePanelView GetPanel(UIPanelType type)
        {
            if (m_type2Panel.ContainsKey(type)) //如果已经实例化过此type的Panel
            {
                return m_type2Panel[type];
            }
            UIPanelInfo info;
            if (m_type2Info.TryGetValue(type, out info)) //获取此type的UIPanel所在的prefab名字
            {
                GameObject creatPanel = GameObject.Instantiate(
                    MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(info.AssetBundlePath).
                        LoadAsset<GameObject>(info.Name)); //创建该物体
                m_type2Panel.Add(type, creatPanel.GetComponent<BasePanelView>());
                return creatPanel.GetComponent<BasePanelView>();
            }
            else //如果在配置表中没有找到此type对应的UIPanel
            {
                Debug.LogError("试图通过 " + type + " 创建当前Scene不存在或未注册的UIPanel");
                return null;
            }
        }

        /// <summary>
        /// 弹出(即显示)对应type的UIPanel
        /// </summary>
        /// <param name="type">要弹出的UIPanel的type</param>
        public void PushPanel(UIPanelType type)
        {
            if (UIPanelStack == null)
            {
                UIPanelStack = new Stack<BasePanelView>();
            }
            BasePanelView targetPanel = GetPanel(type);
            if (UIPanelStack.Count == 0)
            {
                targetPanel.OnEnter();
                UIPanelStack.Push(targetPanel);
            }
            else if (UIPanelStack.Count > 0)
            {
                UIPanelStack.Peek().OnPause();
                targetPanel.OnEnter();
                if (targetPanel != UIPanelStack.Peek())
                {
                    UIPanelStack.Push(targetPanel);
                }
            }
        }

        /// <summary>
        /// 关闭目前栈顶的UIPanel，即当前Player正在操作的Panel
        /// </summary>
        public void PopPanel()
        {
            if (UIPanelStack == null)
            {
                UIPanelStack = new Stack<BasePanelView>();
            }
            if (UIPanelStack.Count == 0)
            {
                return;
            }
            UIPanelStack.Peek().OnExit();
            UIPanelStack.Pop();
            if (UIPanelStack.Count > 0)
            {
                UIPanelStack.Peek().OnResume();
            }
        }

        public void PopAllPanel()
        {
            if (UIPanelStack == null)
            {
                return;
            }
            while (UIPanelStack.Count > 0)
            {
                PopPanel();
            }
        }

    }
}
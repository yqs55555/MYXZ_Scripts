/*
 * FileName             : LoadingScenePanelView.cs
 * Author               : yqs
 * Creat Date           : 2018.1.29
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 加载中的UIPanel,显示加载进度
    /// </summary>
    public class LoadingScenePanelView : BasePanelView
    {
        /// <summary>
        /// 要加载的场景名字
        /// </summary>
        [NonSerialized] public string TargetSceneName;

        /// <summary>
        /// 进度条
        /// </summary>
        private Slider mSlider;

        public Signal<AsyncOperation> LoadingFinish = new Signal<AsyncOperation>();
        private AsyncOperation mLoadSceneAsync;

        public override void OnEnter()
        {
            base.OnEnter();
            mSlider = GetComponentInChildren<Slider>();
            StartCoroutine(LoadScene()); //开启协程异步加载场景
        }

        void Update()
        {
            mSlider.handleRect.transform.Rotate(new Vector3(0, 0, 3f), Space.Self);
        }

        IEnumerator LoadScene()
        {
            float showProgress = 0; //0~100
            mLoadSceneAsync = SceneManager.LoadSceneAsync(TargetSceneName);
            mLoadSceneAsync.allowSceneActivation = false; //关闭场景加载完成自动跳转

            while (mLoadSceneAsync.progress < 0.9f) //到0.9时场景加载已经完成
            {
                if (showProgress < mLoadSceneAsync.progress) //提供更好的视觉效果
                {
                    showProgress += 0.01f;
                    mSlider.value = showProgress;
                    yield return null;
                }
            }

            while (showProgress <= 1.0f)
            {
                showProgress += 0.01f;
                mSlider.value = showProgress;
                yield return null;
            }
            GC.Collect();               //场景切换时强制调用一次GC
            yield return new WaitForSeconds(0.5f);
            LoadingFinish.Dispatch(this.mLoadSceneAsync);   //场景加载完成
        }

        IEnumerator WaitForSceneOpen()
        {
            while (true)
            {
                if (MYXZUIManager.Instance.UIPanelStack.Peek() is WorldSpaceBackGroundPanelView ||
                    MYXZUIManager.Instance.UIPanelStack.Peek() is GameStartMenuPanelView)
                {
                    base.OnExit();
                    break;
                }
                yield return null;
            }
        }

        public override void OnExit()
        {
            StartCoroutine(WaitForSceneOpen());
        }
    }
}
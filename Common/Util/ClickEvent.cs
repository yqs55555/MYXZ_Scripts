/*
 * FileName             : ClickEvent.cs
 * Author               : yqs
 * Creat Date           : 2019.1.6
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MYXZ
{
    public class ClickEvent
    {
        public static Camera MainCamera
        {
            get
            {
                if (m_mainCamera == null)
                {
                    m_mainCamera = Camera.main;
                }

                return m_mainCamera;
            }
        }

        private static Camera m_mainCamera;

        /// <summary>
        /// 鼠标点击时是否点中了目标tag的物体
        /// </summary>
        /// <param name="button">按下的鼠标的哪个键</param>
        /// <param name="tag">目标tag</param>
        /// <param name="maxDistance">从摄像机出发的最远点击距离</param>
        /// <returns>鼠标集中的物体，未击中则为null</returns>
        public static GameObject OnMouseClickTag(int button, string tag, float maxDistance)
        {
            if (Input.GetMouseButtonDown(button) && !EventSystem.current.IsPointerOverGameObject()) //如果此时点击的不是UI
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, maxDistance)) //从鼠标向前发射一条长度为TalkDistance的射线
                {
                    if (hitInfo.transform.CompareTag(tag)) //如果碰到了目标tag
                    {
                        return hitInfo.transform.gameObject;
                    }
                }
            }

            return null;
        }
    }
}
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
        public static GameObject OnMouseClickTag(int button, string tag, float maxDistance)
        {
            if (Input.GetMouseButtonDown(button) && !EventSystem.current.IsPointerOverGameObject()) //如果此时点击的不是UI
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, maxDistance)) //从鼠标向前发射一条长度为TalkDistance的射线
                {
                    if (hitInfo.transform.CompareTag(tag)) //如果碰到了NPC
                    {
                        return hitInfo.transform.gameObject;
                    }
                }
            }

            return null;
        }
    }
}
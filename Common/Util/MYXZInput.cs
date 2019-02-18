/*
 * FileName             : MYXZInput.cs
 * Author               : yqs
 * Creat Date           : 2018.3.27
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
    /// 封装了输入，IsEnable返回当前是否接受输入
    /// </summary>
    public class MYXZInput
    {
        /// <summary>
        /// 是否接收输入
        /// </summary>
        public static bool IsEnable
        {
            get { return mIsEnable; }
            set { mIsEnable = value; }
        }

        private static bool mIsEnable = true;

        public static bool GetKey(KeyCode keyCode)
        {
            return IsEnable && Input.GetKey(keyCode);
        }

        public static bool GetKeyDown(KeyCode keyCode)
        {
            return IsEnable && Input.GetKeyDown(keyCode);
        }

        public static bool GetKeyUp(KeyCode keyCode)
        {
            return IsEnable && Input.GetKeyUp(keyCode);
        }

        public static bool GetMouseButton(int button)
        {
            return IsEnable && Input.GetMouseButton(button);
        }

        public static bool GetMouseButtonDown(int button)
        {
            return IsEnable && Input.GetMouseButtonDown(button);
        }

        public static bool GetMouseButtonUp(int button)
        {
            return IsEnable && Input.GetMouseButtonUp(button);
        }
    }
}
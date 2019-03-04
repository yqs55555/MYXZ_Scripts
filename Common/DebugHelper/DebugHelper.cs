/*
 * FileName             : DebugHelper.cs
 * Author               : yqs
 * Creat Date           : 2019.2.25
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
    public class DebugHelper
    {
        public static bool Assertion(bool passCondition, string errorMsg)
        {
            if (!passCondition)
            {
                Log(errorMsg, DebugType.Error);
            }

            return passCondition;
        }

        public static void Log(string msg, DebugType type = DebugType.Log)
        {
            switch (type)
            {
                case DebugType.Log:
                    Debug.Log(msg);
                    break;
                case DebugType.Error:
                    Debug.LogError(msg);
                    break;
                default:
                    break;
            }
        }

        public static void Log(string msg, GameObject gameObject, DebugType type = DebugType.Log)
        {
            switch (type)
            {
                case DebugType.Log:
                    Debug.Log(msg, gameObject);
                    break;
                case DebugType.Error:
                    Debug.LogError(msg, gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
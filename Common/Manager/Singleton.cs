/*
 * FileName             : Singleton.cs
 * Author               : 
 * Creat Date           : 
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
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T m_instance = null;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject go = GameObject.Find("MYXZSingleton");
                    if (go == null)
                    {
                        go = new GameObject("MYXZSingleton");
                        DontDestroyOnLoad(go);
                    }

                    m_instance = go.GetComponent<T>();

                    if (m_instance == null)
                    {
                        m_instance = go.AddComponent<T>();
                    }
                }

                return m_instance;
            }
        }
    }
}
/*
 * FileName             : MYXZTimer.cs
 * Author               : yqs
 * Creat Date           : 2019.1.5
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MYXZ
{
    /// <summary>
    /// 计时器
    /// </summary>
    public class MYXZTimer : Singleton<MYXZTimer>
    {
        private Dictionary<int, Coroutine> m_masks = new Dictionary<int, Coroutine>();

        IEnumerator DelayFunc(Action func, float delay, int repeat, int mask)
        {
            WaitForSeconds waitTime = new WaitForSeconds(delay);
            while (repeat-- != 0)
            {
                yield return waitTime;
                func();
            }

            m_masks.Remove(mask);
        }

        /// <summary>
        /// 延迟delay时间后执行1次func
        /// </summary>
        /// <param name="func">要执行的函数</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>这个计时器的标记</returns>
        public int AddTimer(Action func, float delay)
        {
            return AddTimer(func, delay, 1);
        }

        /// <summary>
        /// 延迟delay时间后重复执行repeat次func，当repeat小于0时即为不断循环执行
        /// </summary>
        /// <param name="func">要执行的函数</param>
        /// <param name="delay">延迟时间</param>
        /// <param name="repeat">重复执行次数</param>
        /// <returns>这个计时器的标记</returns>
        public int AddTimer(Action func, float delay, int repeat)
        {
            int randomMask;
            do
            {
                randomMask = Random.Range(Int32.MinValue, Int32.MaxValue);
            }
            while (m_masks.Keys.Contains(randomMask));
            Coroutine coro = StartCoroutine(DelayFunc(func, delay, repeat, randomMask));
            m_masks.Add(randomMask, coro);
            return randomMask;
        }

        /// <summary>
        /// 终止标记为mask的Timer
        /// </summary>
        /// <param name="mask">Timer的mask</param>
        public void StopTimer(int mask)
        {
            if (m_masks.Keys.Contains(mask))
            {
                StopCoroutine(m_masks[mask]);
            }
        }
    }
}
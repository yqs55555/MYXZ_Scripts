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
using UnityEngine;
using Random = UnityEngine.Random;

namespace MYXZ
{
    /// <summary>
    /// 计时器，mark为0为预留位
    /// </summary>
    public class MYXZTimer : Singleton<MYXZTimer>
    {
        private Dictionary<int, Coroutine> m_marks = new Dictionary<int, Coroutine>();

        IEnumerator DelayFunc(Action func, float delay, int repeat, int mark)
        {
            WaitForSeconds waitTime = new WaitForSeconds(delay);    
            while (repeat-- != 0)
            {
                yield return waitTime;
                func();
            }

            m_marks.Remove(mark);
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
            int randomMark;
            do
            {
                randomMark = Random.Range(Int32.MinValue, Int32.MaxValue);
            }
            while (m_marks.Keys.Contains(randomMark) || randomMark == 0);
            Coroutine coro = StartCoroutine(DelayFunc(func, delay, repeat, randomMark));
            m_marks.Add(randomMark, coro);
            return randomMark;
        }

        /// <summary>
        /// 终止标记为mark的Timer
        /// </summary>
        /// <param name="mark">Timer的mask</param>
        public void StopTimer(int mark)
        {
            if (m_marks.Keys.Contains(mark))
            {
                StopCoroutine(m_marks[mark]);
            }
        }

        /// <summary>
        /// 标记为mark的协程是否正在运行
        /// </summary>
        /// <param name="mark">协程的标记</param>
        /// <returns>若正在运行返回true</returns>
        public bool IsRunning(int mark)
        {
            return mark != 0 || this.m_marks.ContainsKey(mark);
        }
    }
}
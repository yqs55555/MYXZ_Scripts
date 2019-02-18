/*
 * FileName             : IObserver.cs
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
    public interface IObserver
    {
        void Update(IObservable sender);
    }

    /// <summary>
    /// 当观察者需要从被观察者获取其他参数时
    /// </summary>
    /// <typeparam name="K">被观察者所需要观察的额外参数</typeparam>
    public interface IObserver<K> 
    {
        void Update(IObservable<K> sender, K args);
    }
}
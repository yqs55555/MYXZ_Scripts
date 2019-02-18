/*
 * FileName             : IObservable.cs
 * Author               : 
 * Creat Date           : 
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    public interface IObservable
    {
        void RegisterObserver(Action<IObservable> notifyEventHandler);
        void RemoveObserver(Action<IObservable> notifyEventHandler);
        void NotifyObservers();
    }


    public interface IObservable<T>
    {
        void RegisterObserver(Action<IObservable<T>, T> notifyEventHandler);
        void RemoveObserver(Action<IObservable<T>, T> notifyEventHandler);
        void NotifyObservers(T args);
    }
}
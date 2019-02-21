/*
 * FileName             : IConfigFactory.cs
 * Author               : yqs
 * Creat Date           : 2019.2.19
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
    public interface IConfigFactory<T> : IConfigFactory
    {
        new T Create(string id);
    }

    public interface IConfigFactory
    {
        object Create(string id);
    }
}
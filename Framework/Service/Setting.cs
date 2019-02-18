/*
 * FileName             : Setting.cs
 * Author               : yqs
 * Creat Date           : 2019.2.17
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
    public class Setting
    {
        public class Config
        {
            public static string DEBUG_PATH = "ABResources/Config";
            public static string ROOT = "TotalConfig.xml";
        }
        public class AOI
        {
            public static float GRID_FIELD = 20;
            public static float UPDATE_RATE = 0.2f;
            public static int PLAYER_INTEREST_RADIUS = 3;
            public static int NPC_INTEREST_RADIUS = 2;
        }
    }
}
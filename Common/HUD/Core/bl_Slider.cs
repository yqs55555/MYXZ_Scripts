using System;
using UnityEngine;
using UnityEngine.UI;

public class bl_Slider : MonoBehaviour
{

    public Slider m_Slider = null;
    public RectTransform Rect;

    [HideInInspector] public int m_num;
    [HideInInspector] public Transform m_Transform;
    [HideInInspector] public int m_HealthyPoint;
    [HideInInspector] public int m_Size;
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

public class bl_HUDText : MonoBehaviour
{
    public static bl_HUDText Instance
    {
        get { return mInstance; }
    }

    private static bl_HUDText mInstance;
    /// <summary>
    /// The Canvas Root of scene.
    /// </summary>
    public Transform CanvasParent;
    /// <summary>
    /// UI Prefab to instatantiate
    /// </summary>
    public GameObject TextPrefab;
    public GameObject SliderPrefab;
    [Space(10)]
    public float FadeSpeed;
    public float FloatingSpeed;
    public float HideDistance;
    [Range(0, 180)]
    public float MaxViewAngle;

    public bool DestroyTextOnDeath = true;

    //Privates
    private static List<bl_Text> texts = new List<bl_Text>();
    private static List<bl_Slider> sliders = new List<bl_Slider>();

    private Camera MCamera = null;

    Camera m_Cam
    {
        get
        {
            if (MCamera == null)
            {
                MCamera = (Camera.main != null) ? Camera.main : Camera.current;
            }

            return MCamera;
        }
    }

    void Awake()
    {
        mInstance = this;
    }

    /// <summary>
    /// Disable all text when this script gets disabled.
    /// </summary>
    void OnDisable()
    {
        for (int i = 0; i < sliders.Count; i++)
        {
            if (sliders[i].Rect != null)
            {
                Destroy(sliders[i].Rect.transform);
            }
            sliders[i] = null;
            sliders.Remove(sliders[i]);
        }
        for (int i = 0; i < texts.Count; i++)
        {
            if (texts[i].Rect != null)
            {
                Destroy(texts[i].Rect.gameObject);
            }
            texts[i] = null;
            texts.Remove(texts[i]);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void OnGUI()
    {
        if (m_Cam == null)
        {
            return;
        }

        if (Event.current.type == EventType.Repaint)
        {
            for (int i = 0; i < sliders.Count; i++)
            {
                if (sliders[i].m_Transform == null)
                {
                    //When player / Object death, destroy all last sliders.
                    if (DestroyTextOnDeath)
                    {
                        Destroy(sliders[i].Rect.gameObject);
                        sliders[i] = null;
                    }
                    sliders.Remove(sliders[i]);
                    return;
                }
                //int mov = ScreenPosition(sliders[i].m_Transform);
                //Get center up of target
                Vector3 position = sliders[i].m_Transform.GetComponent<Collider>().bounds.center + (((Vector3.up * sliders[i].m_Transform.GetComponent<Collider>().bounds.size.y) * 0.5f));
                ////its in camera view

                //Convert position to view port
                Vector2 v = MCamera.WorldToViewportPoint(position);

                //    //Apply to slider
                sliders[i].Rect.anchorMax = v;
                sliders[i].Rect.anchorMin = v;
                //sliders[i].GetComponent<RectTransform>().sizeDelta = new Vector2((((mov / 2.0f) * 1.0f) + sliders[i].m_Size), ((mov / 2.0f) * 1.0f + sliders[i].m_Size) / 8.0f);
            }

            for (int i = 0; i < texts.Count; i++)
            {
                //when target is destroyed then remove it from list.
                if (texts[i].m_Transform == null)
                {
                    //When player / Object death, destroy all last text.
                    if (DestroyTextOnDeath)
                    {
                        Destroy(texts[i].Rect.gameObject);
                        texts[i] = null;
                    }
                    texts.Remove(texts[i]);
                    return;
                }
                bl_Text temporal = texts[i];
                //fade text
                if (texts[i].movement == bl_Guidance.Static)
                {
                    temporal.m_Color = texts[i].m_Color;
                }
                else
                {
                    temporal.m_Color -= new Color(0f, 0f, 0f, (Time.deltaTime * FadeSpeed) / 100f);
                }
                //if Text have more than a target graphic
                //add a canvas group in the root for fade all
                if (texts[i].LayoutRoot != null)
                {
                    texts[i].LayoutRoot.alpha = texts[i].m_Color.a;
                }
                //if complete fade remove and destroy text
                if (texts[i].m_Color.a <= 0f && texts[i].movement != bl_Guidance.Static)
                {
                    Destroy(texts[i].Rect.gameObject);
                    texts[i] = null;
                    texts.Remove(texts[i]);
                }
                else//if UI visible
                {
                    //Convert Word Position in screen position for UI
                    int mov = ScreenPosition(texts[i].m_Transform);

                    bl_Text m_Text = texts[i];
                    m_Text.Yquickness += Time.deltaTime * texts[i].YquicknessScaleFactor; ;
                    switch (texts[i].movement)
                    {
                        case bl_Guidance.Up:
                            m_Text.Ycountervail += (((Time.deltaTime * FloatingSpeed) * mov) * 0.25f) * texts[i].Yquickness;
                            break;
                        case bl_Guidance.Down:
                            m_Text.Ycountervail -= (((Time.deltaTime * FloatingSpeed) * mov) * 0.25f) * texts[i].Yquickness;
                            break;
                        case bl_Guidance.Left:
                            m_Text.Xcountervail -= ((Time.deltaTime * FloatingSpeed) * mov) * 0.25f;
                            break;
                        case bl_Guidance.Right:
                            m_Text.Ycountervail += ((Time.deltaTime * FloatingSpeed) * mov) * 0.25f;
                            break;
                        case bl_Guidance.RightUp:
                            m_Text.Ycountervail += (((Time.deltaTime * FloatingSpeed) * mov) * 0.25f) * texts[i].Yquickness;
                            m_Text.Xcountervail += ((Time.deltaTime * FloatingSpeed) * mov) * 0.25f;
                            break;
                        case bl_Guidance.RightDown:
                            m_Text.Ycountervail -= (((Time.deltaTime * FloatingSpeed) * mov) * 0.25f) * texts[i].Yquickness;
                            m_Text.Xcountervail += ((Time.deltaTime * FloatingSpeed) * mov) * 0.25f;
                            break;
                        case bl_Guidance.LeftUp:
                            m_Text.Ycountervail += (((Time.deltaTime * FloatingSpeed) * mov) * 0.25f) * texts[i].Yquickness;
                            m_Text.Xcountervail -= ((Time.deltaTime * FloatingSpeed) * mov) * 0.25f;
                            break;
                        case bl_Guidance.LeftDown:
                            m_Text.Ycountervail -= (((Time.deltaTime * FloatingSpeed) * mov) * 0.25f) * texts[i].Yquickness;
                            m_Text.Xcountervail -= ((Time.deltaTime * FloatingSpeed) * mov) * 0.25f;
                            break;
                        case bl_Guidance.Static:
                            m_Text.Ycountervail = 0;
                            m_Text.Xcountervail = 0;
                            break;

                    }

                    //Get center up of target
                    Vector3 position = texts[i].m_Transform.GetComponent<Collider>().bounds.center + (((Vector3.up * texts[i].m_Transform.GetComponent<Collider>().bounds.size.y) * 0.5f));
                    Vector3 front = position - MCamera.transform.position;
                    //its in camera view
                    if ((front.magnitude <= HideDistance) && (Vector3.Angle(MCamera.transform.forward, position - MCamera.transform.position) <= MaxViewAngle))
                    {
                        //Convert position to view port
                        Vector2 v = MCamera.WorldToViewportPoint(position);
                        //configure each text
                        texts[i].m_Text.fontSize = ((int)(((mov / 2) * 1)) + texts[i].m_Size) / 8;
                        texts[i].m_Text.text = texts[i].m_text;
                        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(texts[i].m_text));

                        //Calculate the movement 
                        Vector2 v2 = new Vector2((v.x - size.x * 0.5f) + texts[i].Xcountervail, -((v.y - size.y) - texts[i].Ycountervail));
                        //Apply to Text
                        texts[i].Rect.anchorMax = v;
                        texts[i].Rect.anchorMin = v;

                        texts[i].Rect.anchoredPosition = v2;
                        texts[i].m_Text.color = texts[i].m_Color;
                        //texts[i].GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Simple way
    /// </summary>
    /// <param name="text"></param>
    /// <param name="trans"></param>
    public void NewText(string text, Transform trans)
    {
        NewText(text, trans, bl_Guidance.Up);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="trans"></param>
    /// <param name="color"></param>
    public void NewText(string text, Transform trans, Color color)
    {
        NewText(text, trans, color, 8, 20f, 1, 2.2f, bl_Guidance.Up);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="trans"></param>
    /// <param name="color"></param>
    /// <param name="place"></param>
    /// <param name="fontSize"></param>
    public void SetHUDText(string text, Transform trans, Color color, bl_Guidance place, int fontSize = 8, float speed = 20.0f)
    {
        NewText(text, trans, color, fontSize, speed, 1, 2.2f, place);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="trans"></param>
    /// <param name="place"></param>
    public void NewText(string text, Transform trans, bl_Guidance place)
    {
        NewText(text, trans, Color.white, 8, 20f, 0, 2.2f, place);
    }
    /// <summary>
    /// send a new event, to create a new floating text
    /// </summary>
    public void NewText(string text, Transform trans, Color color, int size, float speed, float yAcceleration, float yAccelerationScaleFactor, bl_Guidance movement)
    {
        GameObject t = Instantiate(TextPrefab) as GameObject;
        //Create new text info to instatiate 
        bl_Text item = t.GetComponent<bl_Text>();

        item.m_Speed = speed;
        item.m_Color = color;
        item.m_Transform = trans;
        item.m_text = text;
        item.m_Size = size;
        item.movement = movement;
        item.Yquickness = yAcceleration;
        item.YquicknessScaleFactor = yAccelerationScaleFactor;

        t.transform.SetParent(CanvasParent, false);
        t.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        texts.Add(item);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int ScreenPosition(Transform t)
    {
        int p = (int)(m_Cam.WorldToScreenPoint(t.GetComponent<Collider>().bounds.center + (((Vector3.up * t.GetComponent<Collider>().bounds.size.y) * 0.5f))).y - this.m_Cam.WorldToScreenPoint(t.GetComponent<Collider>().bounds.center - (((Vector3.up * t.GetComponent<Collider>().bounds.size.y) * 0.5f))).y);
        return p;
    }

    public void SetHUDActive(List<Transform> activeList)
    {
        foreach (bl_Text blText in texts)
        {
            blText.gameObject.SetActive(false);
            if (activeList.Contains(blText.m_Transform))
            {
                blText.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// send a new event, to create a new HealthyPointSlider
    /// </summary>
    /// <param name="trans">对应角色的transform</param>
    /// <param name="maxValue">对应角色的血量上限</param>
    public void SetHUDSlider(Transform trans, int maxValue, int size)
    {
        GameObject t = Instantiate(SliderPrefab) as GameObject;
        //Create new text info to instatiate 
        bl_Slider item = t.GetComponent<bl_Slider>();

        item.m_Transform = trans;
        item.m_HealthyPoint = maxValue;
        item.m_Slider = t.GetComponent<Slider>();
        item.m_Slider.maxValue = item.m_HealthyPoint;
        item.m_Slider.value = maxValue;

        t.transform.SetParent(CanvasParent, false);
        t.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        sliders.Add(item);
    }

    /// <summary>
    /// get the hp slider of gameobject
    /// </summary>
    /// <param name="trans">对应角色的transform</param>
    public bl_Slider GetHealthyPoint(Transform trans)
    {
        for (int i = 0; i < sliders.Count; i++)
        {
            Debug.Log(sliders[i].m_Transform.name + "    " + trans.name);
            if (sliders[i].m_Transform.name == trans.name)
            {
                return sliders[i];
            }
        }

        Debug.LogError("未找到" + trans.name + "对应的slider");
        return null;
    }

    public void Clear()
    {
        texts = new List<bl_Text>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHelper : MonoBehaviour
{
#if UNITY_EDITOR
    public static bool Touch2 => Input.GetMouseButtonDown(1);
    public static bool Isdown => Input.GetMouseButtonDown(0);
    public static bool Isup => Input.GetMouseButtonUp(0);
    public static Vector2 TouchPosition => Input.mousePosition;
#else
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began);
    public static bool Isdown => Input.GetTouch(0).phase == TouchPhase.Began;
    public static bool Isup => Input.GetTouch(0).phase == TouchPhase.Ended;

    public static Vector2 TouchPosition => Input.GetTouch(0).position;
#endif
    public static bool Touch3 => Input.touchCount == 3 && (Input.GetTouch(2).phase == TouchPhase.Began);
}
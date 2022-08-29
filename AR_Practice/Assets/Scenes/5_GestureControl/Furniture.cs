using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public Camera mainCamera;
    public Transform selectedIcon;

    public bool IsSelected
    {
        get => selectedIcon.gameObject.activeSelf;
        set
        {
            selectedIcon.gameObject.SetActive(value);
        }
    }
    void Start()
    {
        mainCamera = Camera.main;
    }

   
    void Update()
    {
        selectedIcon.transform.LookAt(mainCamera.transform);
    }
}

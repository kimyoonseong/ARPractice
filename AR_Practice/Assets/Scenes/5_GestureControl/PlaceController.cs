using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class PlaceController : MonoBehaviour
{
    public Camera mainCamera;
    public ARRaycastManager raycastManager;
    public GameObject placementIndicator;
    public GameObject[] prefab;
    private Dictionary<int, GameObject> _istancedPrefab = new Dictionary<int, GameObject>();
    void Update()
    {
        var hits = new List<ARRaycastHit>();
        var center = mainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        raycastManager.Raycast(center, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds);
        placementIndicator.SetActive(hits.Count>0);
        if (hits.Count == 0) return;
        var cameraForward = mainCamera.transform.TransformDirection(Vector3.forward);
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
        var pose = hits[0].pose;
        pose.rotation = Quaternion.LookRotation(cameraBearing);
        placementIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation);
        if (TouchHelper.Touch3)
        {
            var index = Random.Range(0, prefab.Length);
            var obj = Instantiate(prefab[index], pose.position, pose.rotation,transform);
            obj.SetActive(true);
            _istancedPrefab[obj.GetInstanceID()] = obj;
            RefreshSelection(obj);
        }
        if (Input.touchCount == 0) return;
        if (TouchHelper.Isdown)
        {
            if(Physics.Raycast(mainCamera.ScreenPointToRay(TouchHelper.TouchPosition),out var raycastHits,
                mainCamera.farClipPlane))
            {
                if (raycastHits.transform.gameObject.tag.Equals("Player"))
                {
                    RefreshSelection(raycastHits.transform.gameObject);
                }
            }
        }
    }
    private void RefreshSelection(GameObject select)
    {
        foreach(var obj in _istancedPrefab)
        {
           var furniture= obj.Value.GetComponent<Furniture>();
            if (furniture)
            {
                furniture.IsSelected = obj.Key == select.GetInstanceID();
            }
        }
    }
}

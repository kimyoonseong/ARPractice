using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class CharacterController : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private Animator _anim;
    protected Transform destination;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.speed = 1.5f;
        destination = GameObject.FindWithTag("Player").transform;
        _raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
     
    }

 
    void Update()
    {
        if (Input.touchCount == 0) return;

        var hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(TouchHelper.TouchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        if (hits.Count == 0) return;
        destination.transform.position = hits[0].pose.position;
        Rotate();
        MoveTo(hits[0].pose.position);
    }
    protected virtual void Rotate()
    {
        var direction = destination.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

    }
    protected virtual void MoveTo(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
    }
}

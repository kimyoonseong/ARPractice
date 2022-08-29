using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : BoxController
{
    private Vector2 _inputPositionPivot;
    
 
    
   
    protected override void OnHold()
    {
        _inputPositionPivot = InputPosition;
        //base.OnHold();//이렇게하면 boxcontroller.cs의 onhold 실행

    }
    protected override void OnPut(Vector3 pos)
    {
        var rigidbody = HoldingObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        var direction = mainCamera.transform.TransformDirection(Vector3.forward).normalized;
        var delta = (pos.y - _inputPositionPivot.y) * 100f / Screen.height;
        rigidbody.AddForce(4.5f*delta*(direction + Vector3.up));
        HoldingObject.transform.SetParent(null);
        _inputPositionPivot.y = pos.y;
    }
}

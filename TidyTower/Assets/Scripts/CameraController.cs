using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _Hedef;


    float _TakipYumusakligi = 0.02f;
    float _OffSetY=2.5f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(
                transform.position.x,
                _Hedef.position.y + _OffSetY,
                transform.position.z),
            _TakipYumusakligi);
    }
}

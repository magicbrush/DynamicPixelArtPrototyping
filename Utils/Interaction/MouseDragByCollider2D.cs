using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class MouseDragByCollider2D : MonoBehaviour {
    public Camera _Cam;
    public int _MouseBtn = 0;
    public Transform _TFRef;
    public float _ZBias = -100.0f;
    public float _LerpSpd = 1.0f;
    public Transform _TgtTF;

    public UnityEvent _MouseDown;
    public UnityEvent _MouseDrag;
    public UnityEvent _MouseUp;

    public void Update()
    {
        if(_bMouseDragging)
        {
            float lerpT = _LerpSpd * Time.deltaTime;
            LerpToMousePos(lerpT);
        }
    }

    private bool _bMouseDragging = false;
    public void OnMouseDown()
    {
        _bMouseDragging = true;
        JumpToMousePos();
        _MouseDown.Invoke();
    }

    public void OnMouseDrag()
    {
        _MouseDrag.Invoke();
    }

    public void OnMouseUp()
    {
        _bMouseDragging = false;
        _MouseUp.Invoke();
    }

    public void JumpToMousePos()
    {
        LerpToMousePos(1.0f);
    }

    public void LerpToMousePos(float t)
    {
        float z = _TFRef.position.z + _ZBias;
        Camera cam = _Cam;
        if (!cam)
        {
            cam = Camera.main;
        }
        Vector3 posTgt = Utils.MousePos2ZPlane(cam, z);
        Vector3 posNow = _TgtTF.position;
        Vector3 pos = Vector3.Lerp(posNow, posTgt, t);
        _TgtTF.position = pos;
    }


}

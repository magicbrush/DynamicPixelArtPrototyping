using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {
    public Camera _Cam;
    public int _MouseBtn = 0;
    public Transform _TFRef;
    public float _ZBias = -100.0f;
    public float _LerpSpd = 1.0f;

	// Use this for initialization
	void Start () {
		
	}

    private bool _bDragging = false;
	// Update is called once per frame
	void Update () {
        bool bMouseDown = 
            Input.GetMouseButtonDown(_MouseBtn);
        if(bMouseDown)
        {
            _bDragging = true;
            JumpToMousePos();
        }

        bool bMouseUp =
            Input.GetMouseButtonUp(_MouseBtn);
        if(bMouseUp)
        {
            _bDragging = false;
        }

        if(_bDragging)
        {
            float lerpT = _LerpSpd * Time.deltaTime;
            LerpToMousePos(lerpT);
        }
	}

    public void JumpToMousePos()
    {
        LerpToMousePos(1.0f);
    }

    public void LerpToMousePos(float t)
    {
        float z = _TFRef.position.z + _ZBias;
        Camera cam = _Cam;
        if(!cam){
            cam = Camera.main;
        }
        Vector3 posTgt = Utils.MousePos2ZPlane(cam, z);
        Vector3 posNow = transform.position;
        Vector3 pos = Vector3.Lerp(posNow, posTgt, t);
        transform.position = pos;
    }

}

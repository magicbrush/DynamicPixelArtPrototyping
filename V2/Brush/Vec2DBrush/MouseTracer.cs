using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MouseTracer : MonoBehaviour {

    public List<Vector2> _Poss = new List<Vector2>();
    public List<float> _Times = new List<float>();

    public float _DistMultiplier = 1.0f;
    public float _MinDistSqr = 0.1f;
    public int _SampleNum = 5;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool bOverNum = (_Times.Count >= _SampleNum);
        if (bOverNum)
        {
            _Times.RemoveAt(0);
            _Poss.RemoveAt(0);
        }

        bool bFirst = (_Times.Count==0);
        Vector2 pos = GetCurPos();
        if (bFirst)
        {
            AddTrace(pos,Time.realtimeSinceStartup);
        }
        else
        {
            Vector2 prevPos = _Poss[_Poss.Count - 1];
            Vector2 offset = pos - prevPos;
            float distSqr = offset.sqrMagnitude;
            if(distSqr>=_MinDistSqr)
            {
                AddTrace(pos,Time.realtimeSinceStartup);
            }
        }
	}

    public Vector2 GetVelocity()
    {
        if(_Times.Count<_SampleNum)
        {
            return Vector2.zero;
        }

        float dt = _Times[_Times.Count - 1] - _Times[0];
        Vector2 movement = _Poss[_Poss.Count - 1] - _Poss[0];
        Vector2 velocity = movement / dt;
        //print("getVelicity: " + velocity.ToString());
        return velocity;
    }

    public void AddTrace(Vector2 pos, float tNow)
    {
        _Poss.Add(pos);
        _Times.Add(tNow);
    }

    private Vector2 GetCurPos()
    {
        Vector2 mpos = Input.mousePosition;
        float scnSize = GetScreenSize();
        Vector2 mpos01 = _DistMultiplier * mpos / scnSize;
        return mpos01;
    }

    [ContextMenu("ClearTrace")]
    public void ClearTrace()
    {
        _Times.Clear();
        _Poss.Clear();
    }

    public float GetScreenSize()
    {
        float screenWd = (float)Screen.width;
        float screenHt = (float)Screen.height;
        return Mathf.Max(screenHt, screenWd);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : MonoBehaviour {

    [System.Serializable]
    public struct State
    {
        public string _Name; 
        public Vector3 _Position;
        public Vector3 _Scale;
        public Vector3 _RotEuler;
    }

    public List<State> _States = new List<State>();

	public void LoadState(string name)
    {
        foreach(var s in _States)
        {
            if(s._Name==name)
            {
                SetTFByState(s);
            }
        }
    }

    private void SetTFByState(State s)
    {
        transform.localPosition = s._Position;
        transform.localScale = s._Scale;
        transform.localRotation = Quaternion.Euler(s._RotEuler);
    }
}

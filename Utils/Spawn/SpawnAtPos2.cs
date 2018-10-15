using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtPos2 : MonoBehaviour {

    public List<GameObject> _Prefabs = new List<GameObject>();
    public int _ChosenID = 0;
    public Transform _ParentTF;
    public Camera _Cam;
    public Transform _RefTF;
    public float _ZBias = -100.0f;

    public void Chose(int id)
    {
        id = Mathf.Clamp(id, 0, _Prefabs.Count);
        _ChosenID = id;
    }

    public void SpawnAtMousePos(){
        float z = _RefTF.position.z + _ZBias;

        Vector3 BirthPos =
            Utils.MousePos2ZPlane(_Cam, z);

        GameObject newObj = Instantiate(
            _Prefabs[_ChosenID], _ParentTF,true) as GameObject;
        newObj.transform.position = BirthPos;

    }
}

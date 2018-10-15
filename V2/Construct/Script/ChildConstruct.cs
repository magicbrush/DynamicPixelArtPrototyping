using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildConstruct : MonoBehaviour {
    public List<GameObject> _Prefabs = new List<GameObject>();
    public int _BatchCount = 1;

	public void Start()
	{
        ConstructChildren();
    }

    private void ConstructChildren()
    {
        for (int i = 0; i < _BatchCount;i++)
        {
            foreach (var gb in _Prefabs)
            {
                ConstructChild(gb);
            } 
        }

    }

    private void ConstructChild(GameObject prefab){
        GameObject newObj = Instantiate(prefab) as GameObject;
        newObj.transform.SetParent(transform,false);
    }

}

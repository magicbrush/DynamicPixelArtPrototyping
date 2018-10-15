using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBehConstruct : MonoBehaviour {

    public List<MonoBehaviour> _PrefabBehaviours = new List<MonoBehaviour>();

    public void Start()
    {
        ConstructBehaviours();
    }

    private void ConstructBehaviours()
    {
        foreach (var beh in _PrefabBehaviours)
        {
            ConstructBeh(beh);
        }
    }

    private void ConstructBeh(MonoBehaviour behPrefab)
    {
        var newBeh = Utils.CopyComponent(behPrefab, gameObject);
    }

}

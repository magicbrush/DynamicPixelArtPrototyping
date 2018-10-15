using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTFScale : MonoBehaviour {

	public void SetLocalScale(float s)
    {
        transform.localScale = s * Vector3.one;
    }
}

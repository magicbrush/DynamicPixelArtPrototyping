using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PX_SpawnArray : MonoBehaviour {
		public GameObject _Prefab;
		public int _col = 4;
		public int _row = 4;
		public Vector2 _BaseVector0 = Vector2.right;
		public Vector2 _BaseVector1 = Vector2.up;
		public Vector3 _Origin = Vector3.zero;
		public bool _bSpawnAtStart = true;

		// Use this for initialization
		void Start () {
			SpawnArray ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		[ContextMenu("SpawnArray")]
		public void SpawnArray()
		{
			for (int c = 0; c < _col; c++) {
				for (int r = 0; r < _row; r++) {
					Vector3 lPos = 
						(Vector3)(c * _BaseVector0 +  r * _BaseVector1) + _Origin;
					GameObject newObj = Instantiate (_Prefab, transform,false) as GameObject;
					newObj.transform.localPosition = lPos;
					newObj.name = _Prefab.name + "(" + c.ToString () + "," + r.ToString () + ")";
				}
			}

		}
	}
}

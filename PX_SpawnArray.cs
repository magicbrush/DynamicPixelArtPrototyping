using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PX_SpawnArray : MonoBehaviour {
		[Header("用途：用指定的Prefab创建一个阵列")]

		[Header("用来创建阵列的原型物")]
		public GameObject _Prefab;

		[Header("阵列的列数和行数")]
		public int _col = 4;
		public int _row = 4;

		[Header("阵列的行列基向量和原点位置")]
		public Vector2 _BaseVector0 = Vector2.right;
		public Vector2 _BaseVector1 = Vector2.up;
		public Vector3 _Origin = Vector3.zero;

		[Header("是否在一开始时创建阵列")]
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

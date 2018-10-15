using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PXScaleToCamOrthoSize : MonoBehaviour {
		public Camera _Cam;
		public float _Ratio = 1.0f;

        public void SetRatio(float r)
        {
            _Ratio = r;
        }

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			UpdateScale ();
		}

		public void UpdateScale ()
		{
			float oSize = _Cam.orthographicSize;
			float scl = oSize * _Ratio;
			transform.localScale = new Vector3 (scl, scl, 1.0f);
		}

		[ContextMenu("RecordCurrentRatio")]
		public void RecordCurrentRatio()
		{
			float oSize = 
				_Cam.orthographicSize;

			Vector3 scl = transform.localScale;
			float ratio = Mathf.Sqrt(scl.x * scl.y) / oSize ;

			_Ratio = ratio;
		}

	}
}

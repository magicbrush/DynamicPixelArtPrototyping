using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PXExample_Vibrating : MonoBehaviour {

		public int _fetchChannel = 0;
		public float _FreqMultiplier = 5.0f;
		private PixelRef _pixelRef = null;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			bool bOK = CheckPixelRef ();
			if (!bOK) {
				return;
			}

			float value = _pixelRef.GetValue (_fetchChannel);
			float freq = _FreqMultiplier * value;
			float scl = Mathf.Sin (freq * Time.realtimeSinceStartup);
			Vector3 scl3 = scl * Vector3.one;
			transform.localScale = scl3;
		}

		bool CheckPixelRef()
		{
			if (_pixelRef == null) {
				_pixelRef = GetComponentInParent<PixelRef> ();
			}
			return (_pixelRef != null);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//sing UnityEngine.UI;

namespace Tipixel
{
	public class PixelSingleChlValueDisp : MonoBehaviour {

		//private PixelGate _pcoord;
		private PixelRef _pxRef;
		public List<TextMesh> _TxtMshs = new List<TextMesh>();
		public int _ChannelId = 0;
		public float _Alpha = 0.33f;
		//public int _QuantizeLevel = 0;

		public float _inMin=-1.0f, _inMax=1.0f;
		public float _dispMin=0,_dispMax=100.0f;

		public int _FractionNumCount = 0;

		// Use this for initialization
		void Start () {
			if (_pxRef == null) {
				_pxRef = GetComponentInParent<PixelRef> ();
			}
		}
		
		// Update is called once per frame
		void Update () {
			float val= _pxRef.GetValue (_ChannelId);
			//int quantLevel = _pcoord._OutValueQuantizeLevel;
			float inputRange = 2.0f;
			float dispRange = _dispMax - _dispMin;

			float valDisp = dispRange * val / inputRange + _dispMin;
			valDisp = Mathf.Clamp (valDisp, _dispMin, _dispMax);

			string format = "F" + _FractionNumCount.ToString ();
			string valTxt = valDisp.ToString (format);

			//valDisp.ToString(

			foreach (var tm in _TxtMshs) {
				tm.text = valTxt;
				Color cr = tm.color;
				cr.a = _Alpha;
				tm.color = cr;
			}
		}

		float MapValue(float value, float inMin, float inMax, float outMin, float outMax)
		{
			float inBound = inMax - inMin;
			float outBound = outMax - outMin;

			float value01 = (value - inMin) / inBound;
			float valueOut = value01 * outBound + outMin;
			return valueOut;
		}


	}
}

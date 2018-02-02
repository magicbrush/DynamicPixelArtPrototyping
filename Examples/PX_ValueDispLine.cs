using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PX_ValueDispLine : MonoBehaviour {
		private PixelRef _pixel;
		public int _chl = 0;
		public float _InMin = -1.0f,_InMax = 1.0f;
		public Vector3 _Start, _End;
		public float _LerpSpd = 3.0f;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			bool bPRefOK = CheckPixelRef ();
			if (!bPRefOK) {
				TurnLineRenderer (false);
				return;
			}

			Pixel px = _pixel.GetPixel ();
			int chlCnt = px.GetChannelCount ();
			bool bON = (chlCnt > _chl);
			if (!bON) {
				TurnLineRenderer (false);
				return;
			}

			TurnLineRenderer (true);


			float val = _pixel.GetValue (_chl);
			float lerpT = (val + 1.0f) / 2.0f;

			Vector3 endTgt = Vector3.Lerp (_Start, _End, lerpT);

			LineRenderer lr = GetComponent<LineRenderer> ();
			Vector3 pStart = lr.GetPosition (0);
			Vector3 pEnd = lr.GetPosition (1);
			pEnd = Vector3.Lerp (pEnd, endTgt, Time.deltaTime * _LerpSpd);

			lr.SetPosition (0, _Start);
			lr.SetPosition (1, pEnd);
		}


		bool CheckPixelRef ()
		{
			if (_pixel == null) {
				_pixel = GetComponentInParent<PixelRef> ();
			}

			return _pixel != null;
		}

		private void TurnLineRenderer(bool bEnable)
		{
			LineRenderer lr = GetComponent<LineRenderer> ();
			lr.enabled = bEnable;

		}



	}
}

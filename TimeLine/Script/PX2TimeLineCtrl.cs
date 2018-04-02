using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Tipixel
{
	public class PX2TimeLineCtrl : MonoBehaviour {
		
		public PlayableDirector _pDir;

		public float _inMin = -1.0f, _inMax = 1.0f;
		public float _SpeedMin =0.0f, _SpeedMax = 1.0f;
		public float _Delay01Min = 0.0f, _Delay01Max  = 1.0f;

		private PixelRef _pxRef = null;
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			PixelRef pxRef = CheckPXRef ();
			if (!pxRef) {
				return;
			}

			if (!CheckPlayableDirector ()) {
				return;
			}

			float value = pxRef.GetValue (0);

			float speed = Utils.MapValue (value, _inMin, _inMax, _SpeedMin, _SpeedMax);
			float delay = Utils.MapValue (value, _inMin, _inMax, _Delay01Min, _Delay01Max);

			double duration = _pDir.duration;

			_pDir.time = Mathf.Repeat(
				Time.realtimeSinceStartup * speed + (float)duration*delay, (float)duration);
			_pDir.Evaluate ();
		}

		private PixelRef CheckPXRef()
		{
			if (!_pxRef) {
				_pxRef = GetComponentInParent<PixelRef> ();
			}
			return _pxRef;
		}

		private bool CheckPlayableDirector()
		{
			if (!_pDir) {
				_pDir = GetComponentInChildren<PlayableDirector> ();
			}
			return _pDir != null;
		}
	}
}

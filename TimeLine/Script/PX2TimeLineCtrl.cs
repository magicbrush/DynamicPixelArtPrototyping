using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Tipixel
{
	public class PX2TimeLineCtrl : MonoBehaviour {
		[Header("这个部件主要用于将像素的数值转化为动画的播放速度和延时")]
		[Header("用法：把记录好的动画prefab拖到这个物体下即可，也可直接对playableDirector进行制定")]
		[Header("注意：只能控制一个PlayableDirector")]

		[Header("要控制的动画")]
		public PlayableDirector _playableDirector;

		[Header ("像素数值的范围，一般就是-1～+1")]
		public float _inMin = -1.0f;
		public float _inMax = 1.0f;

		[Header ("动画速度范围")]
		public float _SpeedMin = 0.0f;
		public float _SpeedMax = 1.0f;

		[Header ("动画的延时量（范围为0～1")]
		[Range (0, 1.0f)]
		public float _Delay01Min = 0.0f;
		[Range (0, 1.0f)]
		public float _Delay01Max = 1.0f;

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

			double duration = _playableDirector.duration;

			_playableDirector.time = Mathf.Repeat(
				Time.realtimeSinceStartup * speed + (float)duration*delay, (float)duration);
			_playableDirector.Evaluate ();
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
			if (!_playableDirector) {
				_playableDirector = GetComponentInChildren<PlayableDirector> ();
			}
			return _playableDirector != null;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PXEvent_TimePassed : MonoBehaviour {
		public UnityEvent _TimePassed;

		public float _LeftTime = 10.0f;

		public void Update()
		{
			if (_LeftTime < 0.0f) {
				return;
			}
			bool bLeftTime = _LeftTime > 0.0f;
			_LeftTime -= Time.deltaTime;
			bool bLeftTime2 = _LeftTime > 0.0f;
			if (bLeftTime && !bLeftTime2) {
				_TimePassed.Invoke ();
			}
		}

		public void SetLeftTime(float lTime)
		{
			_LeftTime = lTime;

		}

		public void SetLeftTimeEnable(float leftTime)
		{
			_LeftTime = leftTime;
			enabled = true;
		}



	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PXEvent_TurnActivity : MonoBehaviour {

		public UnityEvent _TurnActivity;
		public UnityEvent _Activated;
		public UnityEvent _Deactivated;
		public bool _bDetectingActivityChange = false;


		public void Start()
		{
			_PrevActivity = gameObject.activeSelf;
		}

		private bool _PrevActivity = false;
		public void Update()
		{
			if (!_bDetectingActivityChange) {
				return;
			}

			bool bActive = gameObject.activeSelf;
			if (bActive && !_PrevActivity) {
				_TurnActivity.Invoke ();
				_Activated.Invoke ();
			} else if (!bActive && _PrevActivity) {
				_TurnActivity.Invoke ();
				_Deactivated.Invoke ();
			}
				
			_PrevActivity = bActive;
		}

		[ContextMenu("TurnActivity")]
		public void TurnActivity()
		{
			bool active = gameObject.activeSelf;
			bool active1 = !active;

			gameObject.SetActive (active1);

			_TurnActivity.Invoke ();
			if (active1) {
				_Activated.Invoke ();
			} else {
				_Deactivated.Invoke ();
			}
		}

		public void TurnActivity(bool bActive)
		{
			if (bActive) {
				Activate ();
			} else {
				DeActivate ();
			}
		}

		[ContextMenu("Activate")]
		public void Activate()
		{
			gameObject.SetActive (true);

			_TurnActivity.Invoke ();
			_Activated.Invoke ();
		}

		[ContextMenu("DeActivate")]
		public void DeActivate()
		{
			gameObject.SetActive (false);

			_TurnActivity.Invoke ();
			_Deactivated.Invoke ();
		}
	}
}

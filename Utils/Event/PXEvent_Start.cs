using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PXEvent_Start : MonoBehaviour {

		public UnityEvent _Start;
		// Use this for initialization
		[ContextMenu("Start")]
		void Start () {
			_Start.Invoke ();
		}
	}
}
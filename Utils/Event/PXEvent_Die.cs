using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PXEvent_Die : MonoBehaviour {
		public UnityEvent _Die;
		public void Die()
		{
			_Die.Invoke ();
			Destroy (gameObject);
		}
	}
}

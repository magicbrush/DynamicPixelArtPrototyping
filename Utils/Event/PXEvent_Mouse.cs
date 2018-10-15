using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PXEvent_Mouse : MonoBehaviour {

		[System.Serializable]
		public class EventWithInt: UnityEvent<int>{};
		[System.Serializable]
		public class EventWithVector2: UnityEvent<Vector2>{};

		public EventWithInt _MouseButtonDown, _MouseButtonUp;
		public EventWithInt _MousePressed;
		public EventWithVector2 _MouseScroll;

		public void Update()
		{
			for (int i = 0; i < 3; i++) {
				bool bDown = Input.GetMouseButtonDown (i);
				bool bUp = Input.GetMouseButtonUp (i);
				bool bMouse = Input.GetMouseButton (i);

				if (bDown) {
					_MouseButtonDown.Invoke (i);
				}
				if (bUp) {
					_MouseButtonUp.Invoke (i);
				}
				if (bMouse) {
					_MousePressed.Invoke (i);
				}
			}
			_MouseScroll.Invoke (Input.mouseScrollDelta);

		
		}


	}
}

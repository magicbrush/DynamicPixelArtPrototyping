using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tipixel
{
	public class PXBrush : MonoBehaviour {

		public Slider _slider;

		private PixelRef _pxRef = null;

		public void OnMouseDown()
		{
			Paint ();
		}

		public void OnMouseDrag()
		{
			Paint ();
		}
		public void OnMouseOver()
		{
			if (Input.GetMouseButton (0)) {
				Paint ();
			}
		}

		public void Paint()
		{
			PixelRef pxRef = CheckPXRef ();
			if (!pxRef) {
				return;
			}

			pxRef.SetValue (_slider.value);
		}

		private PixelRef CheckPXRef()
		{
			if (!_pxRef) {
				_pxRef = GetComponentInParent<PixelRef> ();
			}
			return _pxRef;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Tipixel
{
	public class PX2EmojiCtrl0 : MonoBehaviour {
		public EmojiCtrl0 _emojiCtrl;

		private PixelRef _pxRef = null;
		public float _speedFactor = 5.0f;

		public enum Mode
		{
			PHASE,
			SPEED
		}
		public Mode _mode = Mode.PHASE;


		public void Update()
		{
			PixelRef pxRef = CheckPXRef ();
			if (!pxRef) {
				return;
			}

			float val = pxRef.GetValue (0);

			float val01 = Mathf.InverseLerp (-1.0f, 1.0f, val);
			if (_mode == Mode.PHASE) {
				_emojiCtrl._Delay01 = val01;
			} else if (_mode == Mode.SPEED) {
				_emojiCtrl._Speed = val01 * _speedFactor;
			}
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

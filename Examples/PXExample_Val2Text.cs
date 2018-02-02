using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	
	public class PXExample_Val2Text : MonoBehaviour {

		public int _fetchChannel = 0;
		private PixelRef _pixelRef = null;

		public List<string> _texts = new List<string>();

		[System.Serializable]
		public class EventWithString: UnityEvent<string>{};
		public EventWithString _GenText;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			bool bOK = CheckPixelRef ();
			if (!bOK) {
				return;
			}

			float value = _pixelRef.GetValue (_fetchChannel);
			float value01 = (value - (-1.0f)) / 2.0f;

			int textCount = _texts.Count;
			float idStep = 2.0f / (float)textCount;

			int choseId = Mathf.RoundToInt(value01 / idStep);

			string txt = _texts [choseId];

			_GenText.Invoke (txt);
		}

		bool CheckPixelRef()
		{
			if (_pixelRef == null) {
				_pixelRef = GetComponentInParent<PixelRef> ();
			}
			return (_pixelRef != null);
		}



	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tipixel
{
	[RequireComponent(typeof(Text))]
	public class DispValueText : MonoBehaviour {

		public float _inMin = -1.0f, _inMax = 1.0f;
		public float _dispMin = -100.0f, _dispMax = 100.0f;
		public int _FractionNumCount = 0;

		public void DispValue(float val)
		{
			float valDisp = 
				Utils.MapValue (val, _inMin, _inMax, _dispMin, _dispMax);

			string format = "F" + _FractionNumCount.ToString ();
			string valTxt = valDisp.ToString (format);
			Text uiText = GetComponent<Text> ();
			uiText.text = valTxt;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PixelValueIniter_ArithSequence : PixelValueIniter {

		public List<float> _InitValues = new List<float> ();
		public List<float> _Steps = new List<float> ();

		public override void InitValue (ref Pixel p)
		{
			int id = p.GetIDInSiblings ();
			List<float> vals = new List<float> ();
			for (int i = 0; i < _InitValues.Count;i++) {
				float v = _InitValues[i] + (float)id * _Steps[i];
				vals.Add (v);
			}
			p.SetValues (vals);
		}
	}
}
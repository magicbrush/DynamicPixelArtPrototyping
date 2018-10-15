using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PixelValueIniter_FixValue : PixelValueIniter {

		public float [] defaultValues = new float[]{};

		public override void InitValue (ref Pixel p)
		{
			List<float> vals = new List<float> ();
			for (int i = 0; i < defaultValues.Length; i++) {
				float v = defaultValues [i];
				vals.Add (v);
			}
			p.SetValues (vals);
		}

	}
}

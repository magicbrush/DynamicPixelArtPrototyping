using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PixelValueIniter_Random : PixelValueIniter {

		public int _channelCount = 1;
		public float _max=1.0f, _min=-1.0f;

		public override void InitValue (ref Pixel p)
		{
			List<float> values = new List<float> ();
			for (int i = 0; i < _channelCount; i++) {
				float val = Random.Range (_min, _max);
				values.Add (val);
			}
			p.SetValues (values);
		}
	}
}

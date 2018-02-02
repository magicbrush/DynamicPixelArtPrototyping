using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PixelValueIniter : MonoBehaviour {

		virtual public void InitValue(ref Pixel p)
		{
			p.SetValue (0.0f, 0);
		}
	}
}

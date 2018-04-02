using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Utils {

	static public float MapValue(float value, float inMin, float inMax, float outMin, float outMax)
	{
		float inBound = inMax - inMin;
		float outBound = outMax - outMin;

		float value01 = (value - inMin) / inBound;
		float valueOut = value01 * outBound + outMin;
		return valueOut;
	}

}

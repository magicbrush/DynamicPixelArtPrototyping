using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    [System.Serializable]
    public class RangeMapper
    {
        public float _InMin = -1.0f;
        public float _InMax = 1.0f;
        public float _OutMin = -1.0f;
        public float _OutMax = 1.0f;
        public float MapValue(float inValue)
        {
            float outVal =
                Utils.MapValue(inValue, _InMin, _InMax, _OutMin, _OutMax);
            return outVal;
        }

        public float InverseMapValue(float outValue)
        {
            float inValue =
                Utils.MapValue(outValue, _OutMin, _OutMax, _InMin, _InMax);
            return inValue;
        }
    }
}


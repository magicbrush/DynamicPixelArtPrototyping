using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXValue : MonoBehaviour
    {
        private float _DefaultValue = 0.0f;
        public List<float> _Values = new List<float>();

        public int GetChannelCount()
        {
            return _Values.Count;
        }

        public float GetValue(int channel = 0)
        {
            if(channel>=_Values.Count)
            {
                return _DefaultValue;
            }

            return _Values[channel];
        }

        public void LerpValue(float tgtValue, float t=1.0f, int chl = 0)
        {
            float lerpT = Mathf.Clamp01(t);
            if(chl>=0&&chl<_Values.Count)
            {
                _Values[chl] = Mathf.Lerp(_Values[chl], tgtValue, lerpT);
            }
        }


    }

}


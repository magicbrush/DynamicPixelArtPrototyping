using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class TFA_TranslatingACurve : TFA_Base
    {
        public enum CoordMode
        {
            Cartisian,  // x,y,z
            Polar       // radius, theta, gamma
        }

        [Header("Cartisian: x,y,z;     Polar: radius, theta, gamma")]
        public CoordMode _coordMode = CoordMode.Cartisian;

        [Space(10)]
        public List<AnimationCurve> _LocCurves = new List<AnimationCurve>();

        public int _valueChl = 0;
        public RangeMapper _Speed;
        public RangeMapper _Delay;
        public float _Amplitude = 1.0f;

        protected override void ComputeTargetTF(
            PXValue pxValue,
            out Vector3 tgtLocPos,
            out Vector3 tgtLocScale, 
            out Quaternion tgtLocRot)
        {
            base.ComputeTargetTF(
                pxValue, 
                out tgtLocPos, 
                out tgtLocScale,
                out tgtLocRot);

            // adjust timeNow by pixel value
            float timeNow = GetScaledTime();
            float pixelValue = pxValue.GetValue(_valueChl);
            float spd = 
                _Speed.MapValue(pixelValue);
            float delay =
                spd * _Delay.MapValue(pixelValue);
            timeNow = timeNow * spd + delay;

            // compute localPosition
            int chlCount = Mathf.Min(3, _LocCurves.Count);
            if (_coordMode == CoordMode.Cartisian)
            {
                for (int i = 0; i < chlCount; i++)
                {
                    AnimationCurve ACurve = _LocCurves[i];
                    tgtLocPos[i] = ACurve.Evaluate(timeNow);
                }
            }
            else if(_coordMode == CoordMode.Polar)
            {
                Vector3 tgr = Vector3.zero;
                for (int i = 0; i < chlCount; i++)
                {
                    AnimationCurve ACurve = _LocCurves[i];
                    tgr[i] = ACurve.Evaluate(timeNow);
                }
                tgtLocPos = Polar2Cart(tgr);
            }
            tgtLocPos *= _Amplitude;
        }

        // polar: radius, theta, gamma
        public Vector3 Polar2Cart(Vector3 polar){
            float radius = polar[0];
            float theta = polar[1];
            float gamma = polar[2];
            float z = radius * Mathf.Sin(gamma);
            float radiusPlane = radius * Mathf.Cos(gamma);
            float x = radiusPlane * Mathf.Cos(theta);
            float y = radiusPlane * Mathf.Sin(theta);
            Vector3 coord = new Vector3(x, y, z);
            return coord;
        }

    }
}


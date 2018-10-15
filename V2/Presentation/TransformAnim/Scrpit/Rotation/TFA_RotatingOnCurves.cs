using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class TFA_RotatingOnCurves : TFA_Base
    {
        public int _valueChl = 0;
        public RangeMapper _Speed;
        public RangeMapper _Delay;
        public float _Amplitude = 1.0f;

        public List<AnimationCurve> _LocCurves = new List<AnimationCurve>();

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

            float timeNow = GetScaledTime();
            float pixelValue = pxValue.GetValue(_valueChl);
            float spd =
                _Speed.MapValue(pixelValue);
            float delay =
                spd * _Delay.MapValue(pixelValue);
            timeNow = timeNow * spd + delay;

            int chlCount = Mathf.Min(3, _LocCurves.Count);
            Vector3 locEuler = tgtLocRot.eulerAngles;
            for (int i = 0; i < chlCount; i++)
            {
                AnimationCurve ACurve = _LocCurves[i];
                locEuler[i] = _Amplitude * ACurve.Evaluate(timeNow);
            }
            tgtLocRot.eulerAngles = locEuler;
        }
    }

}

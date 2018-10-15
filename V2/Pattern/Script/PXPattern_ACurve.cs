using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXPattern_ACurve : PXPattern
    {
        public List<AnimationCurve> _ChannelCurves = new List<AnimationCurve>();
        public override void SetPXValue(PXValue pxValue, float id01)
        {
            for (int chl = 0; chl < _ChannelCurves.Count;chl++)
            {
               float value = _ChannelCurves[chl].Evaluate(id01);
                pxValue.LerpValue(value, 1.0f, chl);
            }
        }
    }
}


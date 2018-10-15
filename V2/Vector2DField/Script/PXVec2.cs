using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXVec2 : MonoBehaviour
    {
        public int _chl0 = 0, _chl1 = 1;

        public RangeMapper _RadiusRange;
        public RangeMapper _ThetaRangeDegs;

        private Vector2 _Vec2D = Vector2.zero;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateVec2D();
        }

        public void UpdateVec2D()
        {
            PXValue pxValue = GetComponentInParent<PXValue>();

            float vr = pxValue.GetValue(_chl0);
            float vt = pxValue.GetValue(_chl1);

            float radius = _RadiusRange.MapValue(vr);
            float theta = _ThetaRangeDegs.MapValue(vt);
 
            float vx = radius * Mathf.Cos(theta);
            float vy = radius * Mathf.Sin(theta);
            _Vec2D = new Vector2(vx, vy);
        }

        public Vector2 GetVec2D()
        {
            return _Vec2D;
        }

    }

}

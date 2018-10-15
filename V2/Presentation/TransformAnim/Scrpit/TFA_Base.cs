using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class TFA_Base : MonoBehaviour
    {
        public float _TimeSale = 1.0f;
        public float _LerpSpd = 1.0f;

        public void Update()
        {
            PXValue pxValue = GetComponentInParent<PXValue>();
            if(!pxValue)
            {
                return;
            }

            // compute target transform
            Vector3 tgtLPos = transform.localPosition;
            Vector3 tgtLScale = transform.localScale;
            Quaternion tgtLRot = transform.localRotation;
            ComputeTargetTF(pxValue, out tgtLPos, out tgtLScale, out tgtLRot);

            // lerp to target transform
            float lerpT = _LerpSpd * Time.deltaTime;
            LerpTF(tgtLPos, tgtLScale, tgtLRot, lerpT);
        }

        public void SetTimeScale(float ts){
            _TimeSale = ts;
        }

        public float GetTimeScale()
        {
            return _TimeSale;
        }

        public float GetScaledTime()
        {
            return _TimeSale * Time.realtimeSinceStartup;
        }

        public void SetLerpSpeed(float lerpSpd)
        {
            _LerpSpd = Mathf.Clamp(lerpSpd,0.0f,float.MaxValue);
        }

        virtual protected void ComputeTargetTF(
            PXValue pxValue,
            out Vector3 tgtLocPos, 
            out Vector3 tgtLocScale,
            out Quaternion tgtLocRot)
        {
            tgtLocPos = transform.localPosition;
            tgtLocScale = transform.localScale;
            tgtLocRot = transform.localRotation;
        }

        protected void LerpTF(
            Vector3 tgtLocPos,
            Vector3 tgtLocScale,
            Quaternion tgtLocRot,
            float t)
        {
            LerpLocPos(tgtLocPos, t);
            LerpLocScale(tgtLocScale, t);
            LerpLocRotation(tgtLocRot, t);
        }

        protected void LerpLocPos(Vector3 tgtLocPos, float t)
        {
            transform.localPosition = 
                Vector3.Lerp(transform.localPosition, tgtLocPos, t);
        }

        protected void LerpLocScale(Vector3 tgtLocScale, float t)
        {
            transform.localScale =
                Vector3.Lerp(transform.localScale, tgtLocScale, t);
        }

        protected void LerpLocRotation(Quaternion tgtLocRot, float t)
        {
            transform.localRotation =
                Quaternion.Lerp(transform.localRotation, tgtLocRot, t);
        }

    }
}


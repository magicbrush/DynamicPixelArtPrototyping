using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXPlacer : MonoBehaviour
    {
        private Transform _TF = null;
        public float _LerpSpd = 3.0f;

        public virtual void Update()
        {
            Vector3 locPos = Vector3.zero;
            Vector3 locScl = Vector3.one;
            Quaternion locRot = Quaternion.identity;
            ComputeTF(out locPos, out locScl, out locRot);

            Transform tf = transform;
            if(_TF!=null){
                tf = _TF;
            }

            float lerpT = Time.deltaTime * _LerpSpd;
            tf.localPosition = 
                Vector3.Lerp(transform.localPosition, locPos, lerpT);
            tf.localScale = 
                Vector3.Lerp(transform.localScale, locScl, lerpT);
            tf.localRotation = 
                Quaternion.Lerp(transform.localRotation, locRot, lerpT);

        }

        protected virtual void ComputeTF(
            out Vector3 LocPos, 
            out Vector3 LocScale, 
            out Quaternion LocRot)
        {
            LocPos = Vector3.zero;
            LocScale = Vector3.one;
            LocRot = Quaternion.identity;
        }



    }
}


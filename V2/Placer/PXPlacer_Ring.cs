using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXPlacer_Ring : PXPlacer
    {
        public PXId2D _pxId2D = null;

        public Vector3 _Origin = Vector3.zero;
        public Vector3 _BaseDir = Vector3.right;
        public float _MaxAngle = Mathf.PI;
        public Vector3 _Axis = Vector3.forward;
        public float _MinRadius = 2.0f;
        public float _RadiusStep = 1.0f;
        public float _ScaleOnRadius = 0.5f;
        public float _BaseScale = 0.5f;
        public Quaternion _BaseRot = Quaternion.identity;
        public Vector3 _RotAxis = Vector3.forward;
        public float _RotOnTheta = 2.0f* Mathf.PI;

        [ContextMenu("CheckID2D")]
        public void CheckID2D()
        {
            if (_pxId2D == null)
            {
                _pxId2D = GetComponent<PXId2D>();
            }
        }

        protected override void ComputeTF(
            out Vector3 LocPos,
            out Vector3 LocScale,
            out Quaternion LocRot)
        {
            base.ComputeTF(out LocPos, out LocScale, out LocRot);

            CheckID2D();

            int cntPerRow = 
                _pxId2D.GetCountPerRow();
            
            Vector2Int cr =
                _pxId2D.GetColRow();

            int thetaStepNum = cntPerRow;
            int radiusStepNum = cr.y;
            float thetaStep = _MaxAngle / (float)thetaStepNum;
            float radiusStep = _RadiusStep;

            float theta = cr.x * thetaStep;
            float radius = cr.y * radiusStep + _MinRadius;

            LocPos = ComputeLocPos(theta, radius);
           // print("theta:" + theta.ToString() +
           //       "      radius:" + radius.ToString());
            LocScale = ComputeLocScale(radius);
            LocRot = ComputeLocRot(theta);

        }

        private Vector3 ComputeLocPos(float theta, float radius)
        {
            Vector3 LocPos;
            //Vector2 offset = Vector2.zero;
            //offset.x = radius * Mathf.Cos(theta);
            // offset.y = radius * Mathf.Sin(theta);
            LocPos = _Origin +
                Quaternion.AngleAxis(theta * Mathf.Rad2Deg, _Axis) * _BaseDir * radius;
            return LocPos;
        }

        private Quaternion ComputeLocRot(float theta)
        {
            Quaternion LocRot;
            float rotAngle = _RotOnTheta * theta;
            LocRot = _BaseRot * Quaternion.AngleAxis(rotAngle * Mathf.Rad2Deg, _RotAxis);
            return LocRot;
        }

        private Vector3 ComputeLocScale(float radius)
        {
            Vector3 LocScale;
            float scl = radius * _ScaleOnRadius + _BaseScale;
            LocScale = Vector3.one * scl;
            return LocScale;
        }


    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXPlacer_Rect : PXPlacer
    {
        public PXId2D _pxId2D = null;

        public Vector3 _Origin = Vector3.zero;
        public Vector3 _XUnitVector = Vector3.right;
        public Vector3 _YUnitVector = Vector3.up;

        [ContextMenu("CheckID2D")]
        public void CheckID2D()
        {
            if(_pxId2D ==null){
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

            Vector2Int cr = 
                _pxId2D.GetColRow();

            LocPos = _Origin + 
                    cr.x * _XUnitVector +
                    cr.y * _YUnitVector;
		}
	}
}


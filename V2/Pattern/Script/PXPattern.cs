using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXPattern : MonoBehaviour
    {
        public Transform _PixelsParent;
        public void SetValues()
        {
            PXValue [] pxValues = 
             _PixelsParent.GetComponentsInChildren<PXValue>();
            int cnt = pxValues.Length;
            foreach(var pv in pxValues)
            {
                var pxId = pv.GetComponent<PXid>();
                float id01 = (float)pxId.GetID() / (float) cnt;
                SetPXValue(pv,id01);
            }
        }

        virtual public void SetPXValue(PXValue pxValue, float id01){

        }
    }

}


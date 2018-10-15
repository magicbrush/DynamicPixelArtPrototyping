using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    [RequireComponent(typeof(Collider2D))]
    public class PXVec2Collider : MonoBehaviour
    {
        public PXVec2 _pxVec2;
        public PXVec2 GetPXVec2()
        {
            return _pxVec2;
        }
    }
}


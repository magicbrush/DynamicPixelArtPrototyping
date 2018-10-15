using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXid : MonoBehaviour
    {
        
        public int GetID()
        {
            int id = transform.GetSiblingIndex();
            return id;
        }

    }
}


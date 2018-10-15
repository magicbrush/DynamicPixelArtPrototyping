using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXVec2DDisp : MonoBehaviour
    {
        public PXVec2 pxVec2 = null;
        public Vector2 _BaseVec = Vector2.right;
        public float _MinRadius = 0.2f;
        public float _RadiusMultiplier = 1.0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateTF();
        }

        public void UpdateTF()
        {
            Vector2 v2 = pxVec2.GetVec2D();

            transform.localScale =
                         (_MinRadius + 
                          _RadiusMultiplier * v2.magnitude ) * Vector2.one;
            transform.localRotation =
                         Quaternion.FromToRotation(_BaseVec, v2);
            transform.localPosition = Vector3.zero;
        }


    }
}


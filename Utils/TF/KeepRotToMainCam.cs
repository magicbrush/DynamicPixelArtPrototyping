using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelV2
{
    public class KeepRotToMainCam : MonoBehaviour
    {
        public float _UpdatePeriod = 0.1f;

        public UnityEvent _Updated;
        private Quaternion _rot = Quaternion.identity;

        // Use this for initialization
        void Start()
        {
            //RecordRot ();
        }

        // Update is called once per frame
        public float _LastUpdateTime = 0.0f;
        void Update()
        {
            float T = Time.realtimeSinceStartup;
            float dt = T - _LastUpdateTime;
            if (dt > _UpdatePeriod)
            {
                UpdateRotation();
                _LastUpdateTime = T;
            }

        }

        [ContextMenu("UpdateRotation")]
        public void UpdateRotation()
        {
            Transform tfParentNow = transform.parent;
            transform.SetParent(Camera.main.transform, true);
            transform.localRotation = _rot;
            transform.SetParent(tfParentNow, true);
            _Updated.Invoke();
        }

        [ContextMenu("RecordRot")]
        public void RecordRot()
        {
            Transform tfParentNow = transform.parent;
            transform.SetParent(Camera.main.transform, true);

            _rot = transform.localRotation;

            transform.SetParent(tfParentNow, true);
        }

    }
}


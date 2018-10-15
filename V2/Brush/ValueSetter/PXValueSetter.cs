using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXValueSetter : MonoBehaviour
    {
        static public bool _bEnableInteraction = true;
        static public List<float> _Values = new List<float>();
        static float _LerpSpd = 1.0f;
        private PXValue _pxValue = null;


        // ----------- set static props ---------------------------------//
        public void TurnAllEnableInteraction(bool bEnable )
        {
            _bEnableInteraction = bEnable;
        }

        public void InitValues(int chl = 3)
        {
            _Values.Clear();
            for (int i = 0; i < chl;i++)
            {
                _Values.Add(0.0f);
            }
        }

        public void SetDefaultValue(float val, int chl = 0)
        {
            if(chl>=0 && chl<_Values.Count)
            {
                _Values[chl] = val;
            }
        }

        public void SetDefaultValue0(float val)
        {
            SetDefaultValue(val, 0);
        }

        public void SetDefaultValue1(float val)
        {
            SetDefaultValue(val, 1);
        }

        public void SetDefaultValue2(float val)
        {
            SetDefaultValue(val, 2);
        }

        public void SetLerpSpd(float lerpSpd)
        {
            lerpSpd = Mathf.Clamp(lerpSpd, 0, float.MaxValue);
            _LerpSpd = lerpSpd;
        }

        // -----------  ---------------------------------//
        private bool _bMousePressed = false;
		public void Update()
		{
            if (!_bEnableInteraction)
            {
                return;
            }

            List<float> vals = new List<float>();
            for (int i = 0; i < _Values.Count; i++)
            {
                vals.Add(_Values[i]);
            }
            float lerpT = Time.deltaTime * _LerpSpd;

            LerpSetValues(vals, lerpT);
        }

        public void LerpSetValues(List<float> values, float lerpT)
        {
            PXValue pXValue = CheckPXValue();
            if (!pXValue)
            {
                return;
            }

            if (_bMousePressed)
            {
                for (int chl = 0; chl < values.Count; chl++)
                {
                    float v = values[chl];
                    pXValue.LerpValue(v, lerpT, chl);
                }
            }
		}

		public void OnMouseDown()
		{
            if (!_bEnableInteraction)
            {
                return;
            }
            _bMousePressed = true;
           // print("OnMosueDown!");
		}

		public void OnMouseUp()
		{
            if (!_bEnableInteraction)
            {
                return;
            }
            _bMousePressed = false;
            //print("OnMosueUp!");
		}

        public PXValue CheckPXValue()
        {
            if(_pxValue==null)
            {
                _pxValue = GetComponentInParent<PXValue>();
            }

            return _pxValue;
        }


	}  
}


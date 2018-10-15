using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    [RequireComponent(typeof(TextMesh))]
    public class PXValueDisp_SIngleChannel : MonoBehaviour
    {
        public int chlId = 0;

        public float _InputMin = -1.0f;
        public float _InputMax = 1.0f;
        public float _DispMin = -100.0f;
        public float _DispMax = 100.0f;
        public int _FracNum = 1;

        public float _UpdateRate = 0.2f;

        public Gradient _ColorGrad;

        // Use this for initialization
        void Start()
        {
            InvokeRepeating("UpdateText", 0.1f, _UpdateRate);
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        private void UpdateText()
        {
            PXValue pXValue = GetComponentInParent<PXValue>();
            float val = pXValue.GetValue(chlId);

            float dispVal = Utils.MapValue(
                val,
                _InputMin, _InputMax,
                _DispMin, _DispMax);

            float crT = Utils.MapValue(
                val,
                _InputMin, _InputMax,
                0, 1);
            Color crText = _ColorGrad.Evaluate(crT);

            string Format = "F" + _FracNum.ToString();
            string text = dispVal.ToString(Format);

            TextMesh textMesh = GetComponent<TextMesh>();
            textMesh.text = text;
            textMesh.color = crText;
        }
    }
}


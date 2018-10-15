using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelV2
{
    [RequireComponent(typeof(Collider2D))]
    public class PXAirBrush : MonoBehaviour
    {
        public List<float> _Values = new List<float>();
        public float _Power = 1.0f;
        [Range(0,1.0f)]
        public float _Softness = 1.0f;

        public List<PXValue> _pxValues = new List<PXValue>();

        [System.Serializable]
        public class EventWithFloat : UnityEvent<float> { };
        public EventWithFloat _SoftNessSet;
        public EventWithFloat _PowerSet;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;
            foreach(var pxValue in _pxValues)
            {
                PaintOnPxValue(pxValue, dt);
            }
        }

        public void SetValue0(float val)
        {
            SetValue(val, 0);
        }

        public void SetValue1(float val)
        {
            SetValue(val, 1);
        }

        public void SetValue2(float val)
        {
            SetValue(val, 2);
        }

        public void SetValue3(float val)
        {
            SetValue(val, 3);
        }

        public void SetValue(float val, int chl)
        {
            if(chl>=0&&chl<_Values.Count)
            {
                _Values[chl] = val;
            }
        }

        public void SetPower(float pwr)
        {
            pwr = Mathf.Clamp(pwr, 0.0f, float.MaxValue);
            _Power = pwr;
            _PowerSet.Invoke(_Power);
        }

        public void SetSoftness(float softness01)
        {
            _Softness = Mathf.Clamp01(softness01);
            _SoftNessSet.Invoke(_Softness);
        }

        public void SetInvSoftness(float invSoftness01)
        {
            SetSoftness(1.0f - invSoftness01);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
           // print("OnTriggerEnter2D");
            PXValueSetter valueSetter = 
                collision.GetComponent<PXValueSetter>();
            if (!valueSetter) { return; }
            PXValue pxValue = valueSetter.CheckPXValue();

            _pxValues.Add(pxValue);

        }

        public void OnTriggerStay2D(Collider2D collision)
        {
           // print("OnTriggerStay2D");
            PXValueSetter valueSetter =
               collision.GetComponent<PXValueSetter>();
            if (!valueSetter) { return; }
            PXValue pxValue = valueSetter.CheckPXValue();
            
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            //print("OnTriggerExit2D");
            PXValueSetter valueSetter =
               collision.GetComponent<PXValueSetter>();
            if (!valueSetter) { return; }
            PXValue pxValue = valueSetter.CheckPXValue();

            _pxValues.Remove(pxValue);
        }

        public void PaintOnPxValue(PXValue pXValue, float dt){

            Vector2 Offset =
                (Vector2)(pXValue.transform.position - 
                          transform.position);
            float dist = Offset.magnitude;
            float scl = 0.5f * (transform.localScale.x + transform.localScale.y);
            float dist01 = dist / scl;
            float brAlpha = GetBrushAlpha(dist01, _Softness);
            float alpha = brAlpha * dt * _Power;

            for (int i = 0; i < _Values.Count;i++)
            {
                pXValue.LerpValue(_Values[i], alpha, i);
            }
        }

        public float GetBrushAlpha(float dist01, float softness)
        {
            float startDecayPos = 1.0f - softness;
            float t = 0.0f;
            if(dist01>startDecayPos)
            {
                t = (dist01 - startDecayPos) / (softness + float.Epsilon);
            }

            float alpha = Mathf.Lerp(1.0f, 0.0f, t);
            return alpha;
        }




    }
}


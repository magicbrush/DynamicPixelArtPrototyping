using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelV2
{
    [RequireComponent(typeof(Collider2D))]
    public class PXVec2DBrush : MonoBehaviour
    {
        public MouseTracer _mouseTracer;
        public float _Power = 1.0f;
        [Range(0, 1.0f)]
        public float _Softness = 1.0f;

        public RangeMapper _SpeedMapper, _AngleMapper;

        public List<PXValue> _pxValues = new List<PXValue>();

        [System.Serializable]
        public class EventWithFloat : UnityEvent<float> { };
        public EventWithFloat _SoftNessSet;
        public EventWithFloat _PowerSet;

        // set/get ------------------------------------------------------------
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

        // ----------------- callbacks  ----------------------------------------
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_bDragging)
            {
                float lerpT = _Power * Time.deltaTime;
                Vector2 vel = 
                    _mouseTracer.GetVelocity();
                
                foreach(var pXValue in _pxValues)
                {
                    PaintOnPXValue(lerpT, vel, pXValue);
                }
            }
        }




        // ----------------- brush control  ------------------------------------
        private bool _bDragging = false;
        public void BeginDragging()
        {
            _bDragging = true;

        }

        public void Dragging()
        {

        }

        public void EndDragging()
        {
            _bDragging = false;
        }

        // ----------------- collider  ----------------------------------------
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


        // ----------------- assist  -------------------------------------------
        private void PaintOnPXValue(
             float lerpT, Vector2 vel, PXValue pXValue)
        {
            float val0, val1;
            GetVal2_RadiusTheta(vel, out val0, out val1);

            float alpha = GetPaintAlpha(Time.deltaTime, pXValue);
            float lerpT2 = lerpT * alpha;

            float v00 = pXValue.GetValue(0);
            float v10 = pXValue.GetValue(1);
            Vector2 vec0 = GetVec2FromRT(v00, v10);
            Vector2 vec1 = Vector2.Lerp(vec0, vel, lerpT);

            Vector2 rt = GetRTFromVec2(vec1);
            pXValue.LerpValue(rt.x, 1.0f, 0);
            pXValue.LerpValue(rt.y, 1.0f, 1);
        }
        public RangeMapper _V0ToRadius, _V1ToTheta;
        private Vector2 GetVec2FromRT(float v0, float v1)
        {
            float radius = 
                _V0ToRadius.MapValue(v0);
            float theta = 
                _V1ToTheta.MapValue(v1);
            Vector2 vec = new Vector2(
                radius * Mathf.Cos(theta),
                radius * Mathf.Sin(theta));
            return vec;
        }

        private Vector2 GetRTFromVec2(Vector2 vec)
        {
            float r = vec.magnitude;
            float theta = Mathf.Atan2(vec.y, vec.x);
            float r1 = _V0ToRadius.InverseMapValue(r);
            float t1 = _V1ToTheta.InverseMapValue(theta);
            return new Vector2(r1, t1);
        }

        private void GetVal2_RadiusTheta(
            Vector2 vel, out float val0, out float val1)
        {
            float speed = vel.magnitude;
            float angle = Mathf.Atan2(vel.y, vel.x);
            //print("angle:" + angle.ToString());
            float spd2 = _SpeedMapper.MapValue(speed);
            float angle2 = _AngleMapper.MapValue(angle);

           //print(" angle2:" + angle2.ToString());

            val0 = spd2;
            val1 = angle2;
        }

        private float GetPaintAlpha(float dt, PXValue pXValue)
        {
            Vector2 Offset =
                                (Vector2)(pXValue.transform.position -
                                      transform.position);
            float dist = Offset.magnitude;
            float scl = 0.5f * (transform.localScale.x + transform.localScale.y);
            float dist01 = dist / scl;
            float brAlpha = GetBrushAlpha(dist01, _Softness);
            float alpha = brAlpha * dt * _Power;
            return alpha;
        }

        private float GetBrushAlpha(float dist01, float softness)
        {
            float startDecayPos = 1.0f - softness;
            float t = 0.0f;
            if (dist01 > startDecayPos)
            {
                t = (dist01 - startDecayPos) / (softness + float.Epsilon);
            }

            float alpha = Mathf.Lerp(1.0f, 0.0f, t);
            return alpha;
        }


    }
}


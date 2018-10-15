using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    [RequireComponent(typeof(PXAirBrush))]
    [RequireComponent(typeof(Shapes2D.Shape))]
    public class PXAirBrush2Shape2D : MonoBehaviour
    {
        public float _PowerMin, _PowerMax;
        public float _AlphaMin, _AlphaMax;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateShape2D();
        }

        public void UpdateShape2D()
        {
            Shapes2D.Shape shape = GetComponent<Shapes2D.Shape>();

            PXAirBrush br = GetComponent<PXAirBrush>();
            float soft = br._Softness;
            float power = br._Power;
            power = Mathf.Clamp(power, _PowerMin, _PowerMax);

            Shapes2D.Shape.UserProps Setting = shape.settings;
            Color fillCr = Setting.fillColor;
            float alpha = Utils.MapValue(power, _PowerMin, _PowerMax, _AlphaMin, _AlphaMax);
            fillCr.a = alpha;
            Setting.fillColor = fillCr;
            Setting.gradientStart = 1.0f - soft;

           // print("GradientStart:" + Setting.gradientStart.ToString() +
             //     " Alpha: "+ alpha.ToString());

            shape.settings = Setting;
        }

    }
}


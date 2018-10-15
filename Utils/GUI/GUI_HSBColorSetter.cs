using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUI_HSBColorSetter : MonoBehaviour
{

    public Color _color;

    [System.Serializable]
    public class EventWithColor: UnityEvent<Color>{};
    public EventWithColor _SetColor;


    public void SetH(float h)
    {
        h = Mathf.Clamp01(h);

        float H=0.0f, S = 0.0f, V = 0.0f;
        Color.RGBToHSV(_color, out H, out S, out V);
        H = h;
        _color = Color.HSVToRGB(H, S, V);

        _SetColor.Invoke(_color);
    }

    public void SetS(float s)
    {
        s = Mathf.Clamp01(s);

        float H = 0.0f, S = 0.0f, V = 0.0f;
        Color.RGBToHSV(_color, out H, out S, out V);
        S = s;

        _color = Color.HSVToRGB(H, S, V);

        _SetColor.Invoke(_color);
    }

    public void SetV(float v)
    {
        v = Mathf.Clamp01(v);

        float H = 0.0f, S = 0.0f, V = 0.0f;
        Color.RGBToHSV(_color, out H, out S, out V);
        V = v;

        _color = Color.HSVToRGB(H, S, V);

        _SetColor.Invoke(_color);
    }


}

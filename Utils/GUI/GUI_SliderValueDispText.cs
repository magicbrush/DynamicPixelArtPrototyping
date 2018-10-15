using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;


public class GUI_SliderValueDispText : MonoBehaviour {

    public Slider _Slider = null;
    public int _FracNum = 2;

    public float _DispMin = 0.0f;
    public float _DispMax = 1.0f;

    public string _Prefex;

    public string _Suffix;

    [System.Serializable]
    public class EventWithText: UnityEvent<string>{};
    public EventWithText _UpdateDispText;

	// Update is called once per frame
	void Update () {
        UpdateDispText();
	}

    public void UpdateDispText()
    {
        Slider sd = CheckSlider();
        if (!sd) { return; }
        float min = sd.minValue;
        float max = sd.maxValue;
        float val = sd.value;

        float dispVal = Utils.MapValue(val, min, max, _DispMin, _DispMax);
        string dispFmt = "F" + _FracNum.ToString();
        string dispText = dispVal.ToString(dispFmt);
        dispText = _Prefex + dispText;
        dispText += _Suffix;

        _UpdateDispText.Invoke(dispText);
    }

    public Slider CheckSlider()
    {
        if(_Slider==null){
            _Slider = GetComponent<Slider>();
        }
        return _Slider;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Utils {




    static public float MapValue(float value, float inMin, float inMax, float outMin, float outMax)
	{
		float inBound = inMax - inMin;
		float outBound = outMax - outMin;

		float value01 = (value - inMin) / inBound;
		float valueOut = value01 * outBound + outMin;
		return valueOut;
	}


    static public float Remap(
            float value,
            float inputMin, float inputMax,
            float outputMin, float outputMax)
    {
        float value01 =
            (value - inputMin) / (inputMax - inputMin);
        float valueOut =
            (outputMax - outputMin) * value01 + outputMin;
        return valueOut;
    }

    static public float Repeat(float value, float period, float phase)
    {
        float value1 = value - phase;
        float value2 = Mathf.Repeat(value1, period);
        float value3 = value2 + phase;
        return value3;

    }

    static public int Repeat(int t, int period)
    {
        int outVal = Mathf.RoundToInt(
            Mathf.Repeat((float)t, (float)period));
        return outVal;
    }

    static public List<string> CopyStringList(List<string> list)
    {
        List<string> s2 = new List<string>();
        for (int i = 0; i < list.Count; i++)
        {
            s2.Add(list[i]);
        }
        return s2;
    }

    static public GameObject ChooseGBByName(List<GameObject> gbs, string name)
    {
        GameObject gb = null;
        foreach (var g in gbs)
        {
            if (g.name == name)
            {
                gb = g;
                break;
            }
        }
        return gb;
    }

    static public float GenPoissonDeltaTimeInterval(float lamda, float maxTime)
    {
        bool bGet = false;
        float x = maxTime;
        int cnt = 0;
        do
        {
            x = Random.Range(0.0f, maxTime);
            float prob = Mathf.Exp(-lamda * x);

            float y = Random.value;

            bGet = (y < prob);
            //Debug.Log("prob:" + prob + " x:" + x + " y:" + y);

            if (bGet)
            {
                break;
            }

            cnt++;
            if (cnt > 100)
            {
                break;
            }
        } while (true);

        return x;
    }

    public static Vector3 MousePos2ZPlane(Camera cam, float z)
    {
        Vector3 mouseOnZ = ScreenPos2ZPlane(Input.mousePosition, cam, z);

        return mouseOnZ;
    }

    public static Vector3 ScreenPos2ZPlane(Vector2 screenPos, Camera cam, float z)
    {
        Ray mouseRay = cam.ScreenPointToRay(screenPos);

        float zDist = z - cam.transform.position.z;
        Vector3 mouseOnZ = mouseRay.origin + zDist * mouseRay.direction;

        return mouseOnZ;
    }


    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();

        //if exist,just get
        Component copy = destination.GetComponent(type);
        if (!copy)
        {
            copy = destination.AddComponent(type);
        }
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }



    public static T CopyComponent0<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }


    public static float RandomPoission(float lamda, float maxValue = 100.0f, int maxIteration = 1000)
    {
        int count = 0;
        while (true)
        {
            float X = Random.Range(0.0f, maxValue);
            float Y = Random.value;
            float TestValue = Mathf.Exp(-lamda * X);
            if (Y < TestValue)
            {
                return X;
            }
            if (count >= maxIteration)
            {
                break;
            }
        }
        return maxValue;
    }

    public static void ResetTransform(Transform tf)
    {
        tf.localPosition = Vector3.zero;
        tf.localScale = Vector3.one;
        tf.localRotation = Quaternion.identity;
    }

    public static bool OutsideVisualArea(RectTransform rtf, Transform tf, float border)
    {
        return rtf.sizeDelta.x + rtf.position.x < tf.position.x || rtf.position.x > Screen.width + border;
    }

    public static bool RectTFOverlap(RectTransform rtf0, RectTransform rtf1, Canvas canvas)
    {
        Rect rect0 =
            RectTransformUtility.PixelAdjustRect(rtf0, canvas);
        Rect rect1 =
            RectTransformUtility.PixelAdjustRect(rtf1, canvas);

        //RectTransformUtility.CalculateRelativeRectTransformBounds (rtf0);

        bool bOverlap = rect0.Overlaps(rect1);
        return bOverlap;
    }

    public static bool rectOverlaps(RectTransform rtf1, RectTransform rtf2)
    {
        Rect rect1 = new Rect(rtf1.position.x, rtf1.position.y, rtf1.rect.width, rtf1.rect.height);
        Rect rect2 = new Rect(rtf2.position.x, rtf2.position.y, rtf2.rect.width, rtf2.rect.height);
        bool bOverlap = rect1.Overlaps(rect2);
        if (rtf2.name == "Btn_Shift_JumpingAlongV3Gener_Freq")
        {
            Debug.Log("catch!" + bOverlap.ToString());
        }

        //Debug.Log (rtf2.name);

        return bOverlap;
    }

    /*
    //if your viewport is screen, then keep it as 'null'
    // NOTICE - doesn't consider if the rectangles are rotating,
    //
    // but shoudl work even if canvas's camera ISN'T aligned with world axis :)
    public static bool is_rectTransformsOverlap( Camera cam,
        RectTransform elem,
        RectTransform viewport = null ){
        Vector2 viewportMinCorner;
        Vector2 viewportMaxCorner;

        if(viewport != null) {
            //so that we don't have to traverse the entire parent hierarchy (just to get screen coords relative to screen),
            //ask the camera to do it for us.
            //first get world corners of our rect:
            Vector3[] v_wcorners = new Vector3[4];
            viewport.GetWorldCorners(v_wcorners); //bot left, top left, top right, bot right

            //+ow shove it back into screen space. Now the rect is relative to the bottom left corner of screen:
            viewportMinCorner = cam.WorldToScreenPoint(v_wcorners[0]);
            viewportMaxCorner = cam.WorldToScreenPoint(v_wcorners[2]);
        }
        else {
            //just use the scren as the viewport
            viewportMinCorner = new Vector2( 0, 0 );
            viewportMaxCorner = new Vector2( Screen.width, Screen.height);
        }

        //give 1 pixel border to avoid numeric issues:
        viewportMinCorner += Vector2.one;
        viewportMaxCorner -= Vector2.one;

        //do a similar procedure, to get the "element's" corners relative to screen:
        Vector3[] e_wcorners = new Vector3[4];
        elem.GetWorldCorners(e_wcorners);

        Vector2 elem_minCorner = cam.WorldToScreenPoint(e_wcorners[0]);
        Vector2 elem_maxCorner = cam.WorldToScreenPoint(e_wcorners[2]);

        //perform comparison:
        if(elem_minCorner.x > viewportMaxCorner.x) { return false; }//completelly outside (to the right)
        if(elem_minCorner.y > viewportMaxCorner.y) { return false; }//completelly outside (is above)

        if(elem_maxCorner.x < viewportMinCorner.x) {  return false;  }//completelly outside (to the left)
        if(elem_maxCorner.y < viewportMinCorner.y) {  return false;  }//completelly outside (is below)


        // commented out, but use it if need to check if element is completely inside:
        //Vector2 minDif = viewportMinCorner - elem_minCorner;
       // Vector2 maxDif = viewportMaxCorner - elem_maxCorner;
       // if(minDif.x < 0  &&  minDif.y < 0  &&  maxDif.x > 0  &&maxDif.y > 0) { //return "is completely inside" }


        return true;//passed all checks, is inside (at least partially)
    }
    */

    public static bool Overlaps(RectTransform rtf1, RectTransform rtf2)
    {
        Rect rt1 = rtf1.rect;
        Rect rt2 = rtf2.rect;
        return Contains(rt1, rt2);
    }

    public static bool Contains(Rect rect1, Rect rect2)
    {

        if ((rect1.position.x <= rect2.position.x) &&
            (rect1.position.x + rect1.size.x) >= (rect2.position.x + rect2.size.x) &&
            (rect1.position.y <= rect2.position.y) &&
            (rect1.position.y + rect1.size.y) >= (rect2.position.y + rect2.size.y))
        {

            return true;
        }
        else
        {

            return false;
        }
    }

    public static Vector2 Clamp(Vector2 vec, float min, float max)
    {
        Vector2 v2 = vec;
        v2.x = Mathf.Clamp(vec.x, min, max);
        v2.y = Mathf.Clamp(vec.y, min, max);
        return v2;
    }

    public static Vector2 Clamp01(Vector2 vec)
    {
        Vector2 v2 = Clamp(vec, 0.0f, 1.0f);
        return v2;
    }

    public static Vector2 PingPong(Vector2 vec, float length)
    {
        Vector2 v2 = vec;
        v2.x = Mathf.PingPong(v2.x, length);
        v2.y = Mathf.PingPong(v2.y, length);
        return v2;
    }

    public static Vector2 Repeat(Vector2 vec, float length)
    {
        Vector2 v2 = vec;
        v2.x = Mathf.Repeat(vec.x, length);
        v2.y = Mathf.Repeat(vec.y, length);
        return v2;
    }

    public static void SetGlobalScale(
        Transform transform,
        Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale =
            new Vector3(
                globalScale.x / transform.lossyScale.x,
                globalScale.y / transform.lossyScale.y,
                globalScale.z / transform.lossyScale.z);
    }

    public static float RandByDistribution(AnimationCurve Distrib, float Min, float Max)
    {
        float x = Random.Range(Min, Max);
        float thres = Distrib.Evaluate(x);
        float y = Random.value;

        for (int i = 0; i < 1000; i++)
        {
            if (y <= thres)
            {
                break;
            }
            x = Random.Range(Min, Max);
            thres = Distrib.Evaluate(x);
            y = Random.value;
        }

        return x;
    }

}



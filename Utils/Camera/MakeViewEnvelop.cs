using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class MakeViewEnvelop : MonoBehaviour {
    public Transform _TgtTF;
    public float _SizeMultiplier = 1.2f;
	
    [ContextMenu("EnvelopAll")]
    public void EnvelopAll()
    {
        Camera cam = GetComponent<Camera>();

        Rect r = GetRect(_TgtTF);

        Vector2 center = r.center;
        Vector3 camPos = cam.transform.position;
        camPos.x = center.x;
        camPos.y = center.y;
        cam.transform.position = camPos;

        float size = Mathf.Max(r.width, r.height);
        cam.orthographicSize = _SizeMultiplier * size / 2.0f;
    }

    public Rect GetRect(Transform parentTF)
    {
        Transform[] tfs = 
            parentTF.GetComponentsInChildren<Transform>();

        float xmin = 0.0f;
        float xmax = 0.0f;
        float ymin = 0.0f;
        float ymax = 0.0f;
        foreach(var tf in tfs){
            Vector3 p = tf.position;
            if (p.x < xmin) { xmin = p.x; }
            if (p.x > xmax) { xmax = p.x; }
            if (p.y < ymin) { ymin = p.y; }
            if (p.y > ymax) { ymax = p.y; }
        }

        Rect r = new Rect()
        {
            min = new Vector2(xmin, ymin),
            max = new Vector2(xmax, ymax)
        };
        return r;
    }

}

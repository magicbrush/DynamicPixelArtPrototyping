using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes2D;

public class ShapeFillColor1Options : MonoBehaviour {
    public Shape _shape;

    public List<Color> _Colors = new List<Color>();

    public void ChooseFillColor1(int id)
    {
        if(id>=0 && id<_Colors.Count)
        {
            _shape.settings.fillColor = _Colors[id];
        }
    }

   
}

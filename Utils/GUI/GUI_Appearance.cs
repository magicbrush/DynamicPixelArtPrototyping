using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Appearance : MonoBehaviour {

    public string _Title;
    public Texture2D _AvatarTex;

	public string GetTitle()
    {
        return _Title;
    }

    public Texture2D GetAvatarTex()
    {
        return _AvatarTex;
    }



}

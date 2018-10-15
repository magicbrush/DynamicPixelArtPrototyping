using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PixelV2
{
    public class PXGUI_PatternBtn : MonoBehaviour
    {
        public PXPattern _pattern;
        public Text uiText;
        public Image _uiImage;
        public void LinkPXPattern(PXPattern ptn){
            _pattern = ptn;

            GUI_Appearance appearance = ptn.GetComponent<GUI_Appearance>();
            string title = appearance.GetTitle();
            Texture2D tex = appearance.GetAvatarTex();

            uiText.text = title;
            _uiImage.sprite =
                       Sprite.Create(tex, new Rect(0, 0, 1.0f, 1.0f), Vector2.zero);
            gameObject.name = "PatternBtn_"+title;
        }

        [ContextMenu("ApplyPattern")]
        public void ApplyPattern()
        {
            _pattern.SetValues();
        }
    }
}


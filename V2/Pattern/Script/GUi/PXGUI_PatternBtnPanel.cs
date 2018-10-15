using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXGUI_PatternBtnPanel : MonoBehaviour
    {
        public GameObject _BtnPrefab;
        public Transform _PatternObjTFParent;
        public PXPattern[] _patterns;
        public Transform _BtnTFParent;

        [ContextMenu("GetPatterns")]
        public void GetPatterns()
        {
            _patterns = 
                _PatternObjTFParent.GetComponentsInChildren<PXPattern>();
        }

        [ContextMenu("InitButtons")]
        public void InitButtons()
        {
            foreach(var ptn in _patterns)
            {
                GameObject newBtn = Instantiate(_BtnPrefab) as GameObject;
                newBtn.transform.SetParent(_BtnTFParent);
                PXGUI_PatternBtn ptnBtn = 
                    newBtn.GetComponent<PXGUI_PatternBtn>();
                ptnBtn.LinkPXPattern(ptn);
            }
        }


    }
}


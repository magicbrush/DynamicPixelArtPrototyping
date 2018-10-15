using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXActivityCtrlByName : MonoBehaviour
    {
        public string _Name = "";

        public List<GameObject> _GameObjs = new List<GameObject>();
       
        public void Activate(string name)
        {
            SetActivity(name, true);
        }

        public void Deactivate(string name)
        {
            SetActivity(name, false);
        }

        public void SetActivity(string name, bool bActive)
        {
            if(name!=_Name)
            {
                return;
            }
            foreach(var gb in _GameObjs)
            {
                gb.SetActive(bActive);
            }
        }

        public void TurnActivity(string name)
        {
            if(name!=_Name)
            {
                return;
            }
            foreach (var gb in _GameObjs)
            {
                gb.SetActive(!gb.activeSelf);
            }
        }
    }
}


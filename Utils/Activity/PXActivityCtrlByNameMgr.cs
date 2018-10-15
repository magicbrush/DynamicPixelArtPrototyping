using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXActivityCtrlByNameMgr : MonoBehaviour
    {

        public void Activate(string name)
        {
            PXActivityCtrlByName[] actCtrls = 
                GetComponentsInChildren<PXActivityCtrlByName>();
            foreach(var ac in actCtrls)
            {
                ac.Activate(name);
            }
        }

        public void Deactivate(string name)
        {
            PXActivityCtrlByName[] actCtrls =
                GetComponentsInChildren<PXActivityCtrlByName>();
            foreach (var ac in actCtrls)
            {
                ac.Deactivate(name);
            }
        }

        public void TurnActivity(string name)
        {
            PXActivityCtrlByName[] actCtrls =
                GetComponentsInChildren<PXActivityCtrlByName>();
            foreach (var ac in actCtrls)
            {
                ac.TurnActivity(name);
            }
        }


    }
}


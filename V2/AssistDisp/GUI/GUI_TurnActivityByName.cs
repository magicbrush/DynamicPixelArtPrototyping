using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class GUI_TurnActivityByName : MonoBehaviour
    {
        public PXActivityCtrlByNameMgr _activeCtrlMgr;
        public string _Name;

        public void SetName(string name)
        {
            _Name = name;
        }

        public void Activate()
        {
            _activeCtrlMgr.Activate(_Name);
        }

        public void Deactivate()
        {
            _activeCtrlMgr.Deactivate(_Name);
        }

        public void TurnActivity()
        {
            _activeCtrlMgr.TurnActivity(_Name);
        }

        public void TurnActivity(bool bActive)
        {
            if(bActive)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
    }
}


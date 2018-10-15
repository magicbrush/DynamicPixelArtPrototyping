using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXActivitySingle : MonoBehaviour
    {
        [System.Serializable]
        public class Item
        {
            public string _Name;
            public Tipixel.PXEvent_TurnActivity _TurnActivity;
        }

        public List<Item> _Items = new List<Item>();


        public void ChooseItem(string Name)
        {
            foreach(var item in _Items)
            {
                if(item._Name==Name)
                {
                    item._TurnActivity.Activate();
                }
                else
                {
                    item._TurnActivity.DeActivate();
                }
            }
        }

        public void TurnItem(string Name)
        {
            foreach (var item in _Items)
            {
                if (item._Name == Name)
                {
                    item._TurnActivity.TurnActivity();
                }
                else
                {
                    item._TurnActivity.DeActivate();
                }
            }
        }



    } 
}


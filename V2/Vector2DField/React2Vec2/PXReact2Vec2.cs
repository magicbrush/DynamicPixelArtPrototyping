using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXReact2Vec2 : MonoBehaviour
    {
        protected List<PXVec2> _Vec2s = new List<PXVec2>();

        public void Update()
        {
            React2Vec2s(_Vec2s, Time.deltaTime);
        }

        virtual protected void React2Vec2s(List<PXVec2> vec2s, float dt)
        {

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            PXVec2 pv2 = CheckVec2Collider(collision);
            if(!pv2){
                return;
            }
            TryAddPXVec2(pv2);
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            PXVec2 pv2 = CheckVec2Collider(collision);
            if (!pv2)
            {
                return;
            }

        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            PXVec2 pv2 = CheckVec2Collider(collision);
            if (!pv2)
            {
                return;
            }
            TryRemovePXVec2(pv2);
        }

        public PXVec2 CheckVec2Collider(Collider2D cld)
        {
            PXVec2Collider pvec2Cld = 
                cld.GetComponent<PXVec2Collider>();
            if(pvec2Cld)
            {
                return pvec2Cld.GetPXVec2();
            }
            return null;
        }

        public bool TryAddPXVec2(PXVec2 pv2)
        {
            if(_Vec2s.Contains(pv2))
            {
                return false;
            }
            else
            {
                _Vec2s.Add(pv2);
                return true;
            }
        }

        public bool TryRemovePXVec2(PXVec2 pv2)
        {
            if (_Vec2s.Contains(pv2))
            {
                _Vec2s.Remove(pv2);
                return true;
            }
            else
            {
                return false;
            }
        }


    }

}

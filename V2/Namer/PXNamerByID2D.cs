using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    
    public class PXNamerByID2D : PXNamerBase
    {

		protected override string _Name()
		{
            PXId2D id2D = 
                GetComponent<PXId2D>();
            if(id2D==null){
                return base._Name();
            }

            Vector2Int cr = 
                id2D.GetColRow();
            string newName = cr.ToString();
            return newName;
		}
	}

}


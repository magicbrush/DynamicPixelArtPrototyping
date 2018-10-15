using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXNamerBase : MonoBehaviour
    {
		public void Start()
		{
            Name();
		}

		public void Name()
        {
            string newname = _Name();
            gameObject.name = newname;
        }

        virtual protected string _Name()
        {
            return gameObject.name;
        }
    }
}


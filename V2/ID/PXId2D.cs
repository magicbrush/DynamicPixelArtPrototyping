using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelV2
{
    public class PXId2D : MonoBehaviour
    {
        public int _CountPerRow = 8;

        private Vector2Int _ColRow = new Vector2Int(-1,-1);


       

		public void Start()
		{
            InvokeRepeating("UpdateColRow", 0.0f, 0.33f);
		}

		public Vector2Int GetColRow()
        {
            if(_ColRow.x<0)
            {
                UpdateColRow();
            }

            return _ColRow;
        }

        [ContextMenu("UpdateColRow")]
        public void UpdateColRow()
        {
            int id = transform.GetSiblingIndex();
            int col = id % _CountPerRow;
            int row = (id - col) / _CountPerRow;

            _ColRow = new Vector2Int(col, row);
        }

        public int GetCountPerRow()
        {
            return _CountPerRow;
        }
    }
}


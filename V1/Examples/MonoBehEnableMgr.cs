using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class MonoBehEnableMgr : MonoBehaviour {

		public MonoBehaviour [] _prefabBehav;
		public bool _bAffectActivity = true;

		[ContextMenu("TurnON")]
		public void TurnON()
		{
			Turn (true);
		}
		[ContextMenu("TurnOFF")]
		public void TurnOFF()
		{
			Turn (false);
		}

		public void Turn(bool bON)
		{
			foreach (var m in _prefabBehav) {
				System.Type type = m.GetType ();
				Component[] behs = GetComponentsInChildren (type,true);
				foreach (var b in behs) {
					MonoBehaviour mbh = (MonoBehaviour)b;
					if (mbh) {
						mbh.enabled = bON;
						if (_bAffectActivity) {
							mbh.gameObject.SetActive (bON);
						}
					}
				}
			}
		}
	}
}

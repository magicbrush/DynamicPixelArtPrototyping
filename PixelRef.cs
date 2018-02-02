using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PixelRef : MonoBehaviour {
		private Pixel _PixelRef;

		public string _Coord;
		public List<float> _Values = new List<float> ();

		public int _OutValueQuantizeLevel = 100;
		private int _OutValueQuantizeLevelPrev = -10000;

		public bool _bSetGameObjName = true;
		public string _NamePrefix = "";

		[System.Serializable]
		public class EventWithPixel: UnityEvent<Pixel>{};
		public EventWithPixel _LinkPixel;

		// Use this for initialization
		void Start () {
			SetOutValueQuantizeLevel (_OutValueQuantizeLevel);
		}
		
		// Update is called once per frame
		void Update () {
			bool bOK = CheckPixel ();
			if (!bOK) {
				return;
			}
			int chlCnt = _PixelRef.GetChannelCount ();
			for (int i = 0; i < _Values.Count; i++) {
				if (i < chlCnt) {
					_Values[i] = _PixelRef.GetValue (i);
				}
			}

		}

		private bool CheckPixel()
		{
			if (_PixelRef != null) {
				return true;
			}
			else {
				if (_rootPixel != null) {
					_PixelRef = _rootPixel.GetChildPixel (_idPath);
				}
				return (_PixelRef != null);
			}
		}
			
		private void UpdateCoordString()
		{
			string txt = "";
			//txt += "(";
			txt += _PixelRef.GetIDInSiblings ().ToString ();

			Pixel parentPx = 
				_PixelRef.GetParent ();
			while (parentPx != null) {
				txt = "," + txt;
				txt = parentPx.GetIDInSiblings ().ToString () + txt;
				parentPx = parentPx.GetParent ();
			}

			//txt += "";
			_Coord = txt;

			string prefix = gameObject.name;
			if (_NamePrefix != "") {
				prefix = _NamePrefix;
			}

			if (_bSetGameObjName) {
				gameObject.name = prefix + _Coord;
			}
		}

		private Pixel _rootPixel = null;
		private List<int> _idPath = new List<int> ();
		public void LinkPixel(Pixel pref)
		{
			_PixelRef = pref;
			if (_LinkPixel == null) {
				_LinkPixel = new EventWithPixel ();
			}
			_LinkPixel.Invoke (pref);
			UpdateCoordString ();

			_idPath = pref.GetFullPathId ();
			_rootPixel = pref.GetRootPixel ();
		}

		public Pixel GetPixel()
		{
			return _PixelRef;
		}

		public float GetValue(int z)
		{
			bool bPixelRefExist = CheckPixel ();
			if (!bPixelRefExist) {
				return TPSetting.invalidValue;
			}
			float value = _PixelRef.GetValue (z);
			if (_OutValueQuantizeLevel>0) {
				value = Pixel.QuantizeValue (value, _OutValueQuantizeLevel);
			}
			return value;
		}

		public List<float> GetValues()
		{
			bool bPixelRefExist = CheckPixel ();
			if (!bPixelRefExist) {
				return null;
			}

			List<float> vals = _PixelRef.GetValues ();
			return vals;
		}

		public void SetOutValueQuantizeLevel(int level = 100)
		{
			if (level >= 0) {
				_OutValueQuantizeLevel = level;
			}
		}

		public void UnQuantizeOutput()
		{
			_OutValueQuantizeLevel = 0;
		}




	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PixelConfiger_Single : PixelConfiger {

		private Pixel _RootPixel;

		[Range(-1.0f,1.0f)]
		public List<float> _Values;
		[Header("是否实时更新数值")]
		public bool _bUpdatingValues = false;
		[Header("初始化数值的玩意")]
		public PixelValueIniter _ValueIniter = null;

		[System.Serializable]
		public class EventWithPixel: UnityEvent<Pixel>{};
		[Header("像素创建时触发")]
		public EventWithPixel _PixelCreated;

		override public void InitPixels ()
		{
			_RootPixel = new Pixel (0);

			Pixel pChd = _RootPixel.NewChildPixel ();

			if (_ValueIniter != null) {
				_ValueIniter.InitValue (ref pChd);
			}

			_PixelCreated.Invoke (pChd);
		}

		override public Pixel GetPixel()
		{
			return _RootPixel;
		}

		// Use this for initialization
		void Start () {
			InitPixels ();
		}
		
		// Update is called once per frame
		void Update () {
			if (_bUpdatingValues) {
				Pixel p = 
					_RootPixel.GetChildPixel (0);
				p.SetValues (_Values);
			}
		}
	}
}

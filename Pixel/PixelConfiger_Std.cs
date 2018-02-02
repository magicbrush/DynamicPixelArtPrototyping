using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tipixel
{
	public class PixelConfiger_Std : PixelConfiger {

		public int _pixelCount = 64;
		public PixelValueIniter _pixelValueIniter;

		//public UnityEvent _PixelsInited;
		private Pixel _RootPixel;

		override public void InitPixels ()
		{
			_RootPixel = new Pixel (0);

			_pixelValueIniter.InitValue (ref _RootPixel);

			for (int i = 0; i < _pixelCount; i++) {
				Pixel p = _RootPixel.NewChildPixel ();
				_pixelValueIniter.InitValue (ref p);
			}

			//_PixelsInited.Invoke ();
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
			
		}

		public void SetPixelCount(int cnt)
		{
			_pixelCount = cnt;
		}

		public int GetPixelCount()
		{
			return _pixelCount;
		}


	}
}
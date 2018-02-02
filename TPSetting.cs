using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class TPSetting{
		
		static private float _DefaultValue = 0.0f;

		public static float defaultValue {
			get {
				return _DefaultValue;
			}
			set {
				_DefaultValue = value;
			}
		}

		static private float _InvalidValue =
			float.NegativeInfinity;
		public static float invalidValue {
			get {
				return _InvalidValue;
			}
			set {
				_InvalidValue = value;
			}
		}




	}
}

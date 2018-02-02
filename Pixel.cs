using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class Pixel {
		public int _BirthID;
		private List<float> _Value;

		private Pixel _ParentPixel;
		private List<Pixel> _ChildPixels;

		private float _InputWeight = 1.0f;
		private float _OutputWeight = 1.0f;
		private float _Scale = 1.0f;

		//private int _QuantizeLevel = 0;
		private static int _BirthCount = 0;

		// ------------ constructor ---------------------------------//
		public Pixel(
			float value, 
			float inputWeight = 1.0f, 
			float outputWeight = 1.0f, 
			float scale = 1.0f,
			Pixel parent = null)
		{
			List<float> fList = new List<float> ();
			fList.Add (value);
			Init (fList, inputWeight, outputWeight, 
				scale, parent);
			_BirthCount++;

		}

		public Pixel(
			float [] values,
			float inputWeight = 1.0f, 
			float outputWeight = 1.0f, 
			float scale = 1.0f,
			Pixel parent = null)
		{
			List<float> fList = new List<float> ();
			for (int i = 0; i < values.Length; i++) {
				fList.Add (values [i]);
			}
			Init (fList, inputWeight, outputWeight, 
				scale, parent);
			_BirthCount++;
		}

		public Pixel(List<float> values,
			float inputWeight = 1.0f, 
			float outputWeight = 1.0f, 
			float scale = 1.0f,
			Pixel parent = null)
		{
			Init (values, inputWeight, outputWeight, 
				scale, parent);
			_BirthCount++;
		}

		public Pixel Clone(Pixel parentPixel = null)
		{
			Pixel newPx = new Pixel(_Value,_InputWeight,_OutputWeight,_Scale,null);
			for (int i = 0; i < _Value.Count; i++) {
				newPx._Value.Add (_Value [i]);
			}
			newPx.SetParent (parentPixel);

			int chdCnt = GetChildCount ();
			for (int i = 0; i < chdCnt; i++) {
				Pixel chdPixel = GetChildPixel (i);
				Pixel chdPxClone = chdPixel.Clone (newPx);
			}

			return newPx; 
		}

		public void CopyValueFrom(Pixel RefPixel, bool bCopyTreeStructure = true)
		{
			_Value = RefPixel._Value;
			_InputWeight = RefPixel._InputWeight;
			_OutputWeight = RefPixel._OutputWeight;
			int refChdCnt = RefPixel.GetChildCount ();
			int chdCnt = GetChildCount ();
			for (int i = 0; i < _ChildPixels.Count; i++) {
				if (i < refChdCnt) {
					Pixel refChdPx = RefPixel.GetChildPixel (i);
					Pixel chdPx = GetChildPixel (i);
					float val0 = chdPx.GetValue (0);
					chdPx.CopyValueFrom (refChdPx, bCopyTreeStructure);
					float val1 = chdPx.GetValue (0);
					//Debug.Log ("value Change:" + (val1-val0).ToString());
				}
			}
			if (bCopyTreeStructure) {
				bool bOver = chdCnt > refChdCnt;
				bool bLess = chdCnt < refChdCnt;
				if (bOver) {
					int overCnt = chdCnt - refChdCnt;
					_ChildPixels.RemoveRange (refChdCnt - 1, overCnt);
				} else if (bLess) {
					for (int i = chdCnt; i < refChdCnt; i++) {
						Pixel refChd = RefPixel.GetChildPixel (i);
						Pixel newChdPx = NewChildPixel (refChd._InputWeight, refChd._OutputWeight, refChd._Scale);
						newChdPx.CopyValueFrom (refChd, bCopyTreeStructure);
					}
				}
			}
		}

		// ------------ identity -------------------------------------------//
		public int GetBirthID()
		{
			return _BirthID;
		}

		public int GetIDInSiblings()
		{
			if (_ParentPixel == null) {
				return 0;
			} else {
				System.Predicate<Pixel> pred = IsSamePixelToThis;
				int id = _ParentPixel._ChildPixels.FindIndex (pred);
				return id;
			}
		}

		public bool IsSamePixelToThis(Pixel obj)
		{
			return (obj == this);
		}

		public int GetSiblingsCount()
		{
			if (_ParentPixel == null) {
				return 1;
			} else {
				int chdCnt = _ParentPixel.GetChildCount ();
				return chdCnt;
			}
		}

		public List<int> GetFullPathId()
		{
			List<int> ids = new List<int> ();
			int id = GetIDInSiblings ();
			ids.Add (id);
			Pixel parentPixel = GetParent();
			while (parentPixel != null) {
				id = parentPixel.GetIDInSiblings ();
				ids.Add (id);
				parentPixel = parentPixel.GetParent ();
			}

			ids.Reverse ();
			return ids;
		}

		// ------------ Tree operations --------------------------------------//
		public void SetParent(Pixel parentPixel)
		{
			if (_ParentPixel != null) {
				_ParentPixel._ChildPixels.Remove (this);
			}
			_ParentPixel = parentPixel;
			if (_ParentPixel != null) {
				_ParentPixel._ChildPixels.Add (this);
			}

		}

		public Pixel GetParent()
		{
			return _ParentPixel;
		}

		public Pixel GetRootPixel()
		{
			Pixel parent = GetParent ();
			Pixel pxThis = this;
			while (parent != null) {
				pxThis = parent;
				parent = parent.GetParent ();
			}
			return pxThis;
		}

		public int GetChildCount()
		{
			RemoveNullChild ();
			return _ChildPixels.Count;
		}

		public List<Pixel> GetChildPixels()
		{
			RemoveNullChild ();
			List<Pixel> pxs = new List<Pixel> ();

			foreach (var px in _ChildPixels) {
				pxs.Add (px);
			}

			return pxs;
		}

		public Pixel GetChildPixel(int id)
		{
			if (id >= 0 &&
			    _ChildPixels.Count > 0 &&
			    id < _ChildPixels.Count) {
				return _ChildPixels [id];
			} else {
				return null;
			}
		}

		public Pixel GetChildPixel(List<int> idPaths)
		{
			Pixel pxTracing = this;
			for (int i = 0; i < idPaths.Count; i++) {
				int id = idPaths [i];
				if (id < 0) {
					return null;
				}

				int chdCnt = GetChildCount ();
				if (id >= chdCnt) {
					return null;
				}

				pxTracing = GetChildPixel (id);
			}
			return pxTracing;
		}

		public void SetInputWeight(float inWt)
		{
			inWt = Mathf.Clamp01 (inWt);
			_InputWeight = inWt;
		}

		public float GetInputWeight()
		{
			return _InputWeight;
		}

		public void SetOutputWeight(float outWt)
		{
			outWt = Mathf.Clamp (outWt, 0.0f, float.PositiveInfinity);
			_OutputWeight = outWt;
		}

		public float GetOutputWeight()
		{
			return _OutputWeight;
		}

		public void SetScale(float scale)
		{
			_Scale = scale;
		}
			
		public float GetScale()
		{
			return _Scale;
		}

		// ----- child operation ---------//
		public Pixel NewChildPixel(
			float inputWt = 1.0f,
			float outputWt = 1.0f, 
			float scale = 1.0f)
		{
			List<float> vals = new List<float> ();
			for (int i = 0; i < _Value.Count; i++) {
				vals.Add (_Value [i]);
			}
			Pixel newPx = new Pixel (vals, inputWt, outputWt, scale, this);
			return newPx;
		}

		public bool RemoveChildPixel(Pixel px)
		{
			px._ParentPixel = null;
			bool bRemove = _ChildPixels.Remove (px);
			return bRemove;
		}

		// -------------- get/set values --------------------------------//
		public int GetChannelCount()
		{
			return _Value.Count;
		}

		public List<float> GetValues()
		{
			RemoveNullChild ();
			if (_ChildPixels.Count > 0) {
				InterpFromSubs ();
			}

			List<float> newVals = new List<float> ();
			for (int i = 0; i < _Value.Count; i++) {
				newVals.Add (_Value [i]);
			}
			return newVals;
		}

		public bool SetValues(List<float> values)
		{
			bool bChanged = false;
			bChanged = MakeEnoughChannelCount (values.Count);

			for (int i = 0; i < values.Count; i++) {
				if (_Value [i] != values [i]) {
					bChanged = true;
				}
				_Value [i] = values [i];
			}
			RemoveNullChild ();
			foreach (var px in _ChildPixels) {
				px.SetValues (values);
			}

			return bChanged;
		}

		public float GetValue(int channel=0)
		{
			if (channel < 0) {
				return TPSetting.invalidValue;
			}

			MakeEnoughChannelCount (channel + 1);

			RemoveNullChild ();
			if (_ChildPixels.Count > 0) {
				InterpFromSubs ();
			}

			return _Value [channel];
		}

		public bool SetValue(float value, int channel=0)
		{
			if (channel < 0) {
				return false;
			}

			MakeEnoughChannelCount (channel + 1);

			_Value [channel] = value;
			RemoveNullChild ();
			foreach (var px in _ChildPixels) {
				px._Value [channel] = value;
			}
			return true;
		}

		// ----------------- apply value -----------------------//
		public bool ApplyValue(
			float input, 
			float power = 10000.0f, 
			int channel = 0)
		{
			if (channel < 0) {
				return false;
			}
				
			MakeEnoughChannelCount (channel + 1);

			RemoveNullChild ();
			if (_ChildPixels.Count > 0) {
				foreach (var px in _ChildPixels) {
					px.ApplyValue (input, power, channel);
				}
				InterpFromSubs ();
			} else {
				float alpha2 = power * _InputWeight;
				alpha2 = Mathf.Clamp01 (alpha2);
				_Value [channel] = 
					(1.0f-alpha2) * _Value [channel] + alpha2 * input;
			}

			return true;
		}

		public bool ApplyValues(
			List<float> inputs, 
			float power = 10000.0f)
		{
			MakeEnoughChannelCount (inputs.Count);

			RemoveNullChild ();
			if (_ChildPixels.Count > 0) {
				foreach (var px in _ChildPixels) {
					px.ApplyValues (inputs, power);
				}
				InterpFromSubs ();
			} else {
				float alpha2 = power * _InputWeight;
				alpha2 = Mathf.Clamp01 (alpha2);
				for (int i = 0; i < inputs.Count; i++) {
					_Value [i] = 
						(1.0f - alpha2) * _Value [i] + alpha2 * inputs [i];
				}
			}

			return true;
		}

		// ------------- privates -------------------------------------//
		private void Init(
			List<float> values,
			float inputWeight = 1.0f, 
			float outputWeight = 1.0f, 
			float scale = 1.0f,
			Pixel parent = null)
		{
			_Value = new List<float> ();
			for (int i = 0; i < values.Count; i++) {
				_Value.Add (values [i]);
			}

			_InputWeight = inputWeight;
			_OutputWeight = outputWeight;
			_Scale = scale;

			_ParentPixel = parent;
			if (parent != null) {
				parent._ChildPixels.Add (this);
			}

			_ChildPixels = new List<Pixel> ();

			//Debug.Log ("_Value Count:" + _Value.Count);

			_BirthID = _BirthCount;
		}

		private bool MakeEnoughChannelCount(int count)
		{
			bool bChanged = (_Value.Count != count);

			ExtendValueList (ref _Value, count, TPSetting.defaultValue);
			RemoveNullChild ();
			foreach (var px in _ChildPixels) {
				ExtendValueList (ref px._Value, count, TPSetting.defaultValue);
			}

			return bChanged;
		}

		private bool CheckChannel (int channel)
		{
			if (channel < 0) {
				return false;
			}
			if (channel >= _Value.Count) {
				int exceed = channel - _Value.Count + 1;
				for (int i = 0; i < exceed; i++) {
					_Value.Add (TPSetting.defaultValue);
				}
			}
			return true;
		}
			
		private void InterpFromSubs()
		{
			List<Pixel> chdPxs = GetChildPixels ();
			List<List<float>> childValues = new List<List<float>> ();
			for (int i = 0; i < chdPxs.Count; i++) {
				childValues.Add (chdPxs [i].GetValues ());
			}

			float wtSum = 0.0f;
			for (int i = 0; i < chdPxs.Count; i++) {
				wtSum += chdPxs [i]._OutputWeight;
			}

			List<float> interpedValues = new List<float> ();
			for (int i = 0; i < childValues.Count; i++) {
				interpedValues.Add (TPSetting.defaultValue);
				for (int j = 0; j < childValues [i].Count; j++) {
					interpedValues [i] += childValues [i] [j];
				}
				interpedValues [i] /= wtSum;
			}
			_Value = interpedValues;
		}
			
		private void RemoveNullChild()
		{
			for (int i = _ChildPixels.Count - 1; i >= 0; i--) {
				if (_ChildPixels [i] == null) {
					_ChildPixels.RemoveAt (i);
				}
			}
		}

		static private void ExtendValueList(
			ref List<float> values, 
			int channelCount, 
			float defaultValue)
		{
			int incNum = channelCount -values.Count ;
			for (int i = 0; i < incNum; i++) {
				values.Add (defaultValue);
			}
		}

		static public float QuantizeValue(float value, int quntizeLevel)
		{
			if (quntizeLevel > 0) {
				float val2 = Mathf.Floor (value * quntizeLevel)/quntizeLevel;
				return val2;
			} else {
				return value;
			}
		}


	}
}

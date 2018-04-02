using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Tipixel
{
	public class EmojiCtrl0 : MonoBehaviour {

		public PlayableDirector _pDir;
		public float _Speed = 1.0f;
		public float _Delay01 = 0.0f;
		// Use this for initialization
		void Start () {
			_pDir.extrapolationMode = DirectorWrapMode.Loop;
		}
		
		// Update is called once per frame
		void Update () {
			double dur = _pDir.duration;
			_pDir.time = Mathf.Repeat(
				Time.realtimeSinceStartup * _Speed + (float)dur*_Delay01, (float)_pDir.duration);
			_pDir.Evaluate ();

			PlayState pstate = _pDir.state;

			//print ("state:" + pstate.ToString ());
		}

		[ContextMenu("GetDirector")]
		public void GetDirector()
		{
			_pDir = GetComponent<PlayableDirector> ();
		}

	}
}

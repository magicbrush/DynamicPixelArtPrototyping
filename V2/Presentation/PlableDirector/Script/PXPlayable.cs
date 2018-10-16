using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace PixelV2
{
    //[RequireComponent(typeof(PlayableDirector))]
    public class PXPlayable : MonoBehaviour
    {
        public enum ExtraPolationMode
        {
            Repeat,
            PingPong,
        }
        public enum DelayMode
        {
            Absolute,
            Normalized
        }

        public ExtraPolationMode timeExtraPolationMode = 
            ExtraPolationMode.Repeat;
        public float _TimeScale = 1.0f;
        public int _ChlIdSpeed = 0;
        public RangeMapper _Speed;
        public int _ChlIdDelay = 0;
        public RangeMapper _Delay;
        public DelayMode _delayMode = DelayMode.Absolute;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdatePlayableDirector();
        }

        private void UpdatePlayableDirector()
        {
            PlayableDirector [] playableDirectors =
                            GetComponentsInChildren<PlayableDirector>();
            if(playableDirectors.Length==0)
            {
                return;
            }

            PXValue pxValue = GetComponentInParent<PXValue>();
            if (!pxValue)
            { return; }

            float valSpd = pxValue.GetValue(_ChlIdSpeed);
            float spd = _Speed.MapValue(valSpd);

            float valDelay = pxValue.GetValue(_ChlIdDelay);
            float delay = _Delay.MapValue(valDelay);

            foreach(var playableDirector in playableDirectors)
            {
                if (_delayMode == DelayMode.Normalized)
                {
                    delay = (float)playableDirector.duration * delay;
                }

                double TNow = playableDirector.time;
                double T = _TimeScale * (spd * Time.realtimeSinceStartup + delay);

                double Duration = playableDirector.duration;
                if (timeExtraPolationMode == ExtraPolationMode.PingPong)
                {
                    T = Mathf.PingPong((float)T, (float)Duration);
                }
                else if (timeExtraPolationMode == ExtraPolationMode.Repeat)
                {
                    T = Mathf.Repeat((float)T, (float)Duration);
                }

                playableDirector.time = T;
            }
        }

    }

}

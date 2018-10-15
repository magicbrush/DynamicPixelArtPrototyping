using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace PixelV2
{

    public class PXVideoPlayer : MonoBehaviour
    {
        public int _RTWidth, _RTHeight;
        public RawImage _rawImage;
        public VideoPlayer _videoPlayer;
        public RenderTextureFormat _RTFormat;
        public RenderTexture _RenderTex;
        public int _ChlPlaySpeed = 0;
        public RangeMapper _PlaySpeed;

        public int _ChlDelay = 0;
        public RangeMapper _Delay;

        public bool bConfigVideoPlayer = true;

        // Use this for initialization
        void Start()
        {
            InitRenderTexture();
        }

        // Update is called once per frame
        void Update()
        {
            bool bOK = true;
            if(bConfigVideoPlayer)
            {
                bOK = ConfigVideoPlayer();
            }

            if(!bOK)
            {
                VideoPlayer vp = _videoPlayer;
                if (vp.isPlaying)
                {
                    vp.Stop();
                }
                return;
            }
            StepForwardVideoPlay();
            //print("StepForwardVideoPlay");
        }

        [ContextMenu("InitRenderTexture")]
        public void InitRenderTexture()
        {
            uint wd = _videoPlayer.clip.width;
            uint ht = _videoPlayer.clip.height;
            _RenderTex = new RenderTexture(
                _RTWidth, _RTHeight, 24,
                    _RTFormat);
            _RenderTex.name =
                          _videoPlayer.clip.name +
                          Random.value.ToString();
            _videoPlayer.targetTexture = _RenderTex;

            _rawImage.texture = _RenderTex;
        }

        private void StepForwardVideoPlay()
        {
            VideoPlayer vp = _videoPlayer;
            //vp.StepForward();
            if(!vp.isPlaying)
            {
                vp.Play();
            }
            //vp.Pause();
            //vp.StepForward
        }

        public bool ConfigVideoPlayer()
        {
            PXValue pxValue = GetComponentInParent<PXValue>();
            if (!pxValue)
            { return false; }

            float valSpd = pxValue.GetValue(_ChlPlaySpeed);
            float spd = _PlaySpeed.MapValue(valSpd);

            float valDelay = pxValue.GetValue(_ChlDelay);
            float delay = _Delay.MapValue(valDelay);

            VideoPlayer vp = _videoPlayer;
            double VideoLength = vp.clip.length;
            float t = delay + Time.realtimeSinceStartup;
            t = Mathf.Repeat(t, (float)VideoLength);
            vp.playbackSpeed = spd;
            vp.time = t;
            return true;
        }
    }
}


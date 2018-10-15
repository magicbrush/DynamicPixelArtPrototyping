using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tipixel
{
	public class PXUI_MainCameraCtrl : MonoBehaviour {
		public Camera _MainCam;
		public float _ZoomMin=3.0f, _ZoomMax=20.0f;
		public float _ZoomSpd = 5.0f;

		public float _moveSpdOnOrthoSize = 1.0f;
		public float _panSpdOnOrthoSize = 1.0f;
		public float _LerpSpd = 3.0f;

		public KeyCode _ZoomInKey, _ZoomOutfKey;
		public KeyCode _UpKey, _DownKey, _LeftKey, _RightKey;


		private float _zoom = 0.0f;
		private Vector2 _LeftPanMove = Vector2.zero;

       

		// Use this for initialization
		void Start () {
			
		}

		public void Update()
		{
            float dt = Time.deltaTime;
            Update_Key2Move(dt);
            //PanMove(dt);
            Move(dt);
            Zoom(dt);
		}

        public void Move(float dt)
        {
            _MainCam.transform.position += (Vector3)_moveVel * dt;
        }

        void PanMove(float dt)
        {
            Vector2 MoveVec = Vector3.Lerp(Vector2.zero, _LeftPanMove, dt * _LerpSpd);
            _LeftPanMove -= MoveVec;
            Vector3 pos = _MainCam.transform.position;
            Vector3 pos2 = pos + (Vector3)MoveVec;
            _MainCam.transform.position = pos2;
        }

        void Zoom(float dt)
        {
            float zoomDelta = 0.0f;
            if (Mathf.Approximately(_zoom, 0.0f))
            {
                if (Input.GetKey(_ZoomInKey))
                {
                    zoomDelta -= dt * _ZoomSpd;
                }
                if (Input.GetKey(_ZoomOutfKey))
                {
                    zoomDelta += dt * _ZoomSpd;
                }
            }
            else
            {
                zoomDelta = _zoom * _ZoomSpd * dt;
            }

            if (!Mathf.Approximately(0.0f, zoomDelta))
            {
                ZoomCamera(zoomDelta);
            }
        }

        Vector2 _moveVel = Vector2.zero;
        void Update_Key2Move (float dt)
		{
			float orthoSize = _MainCam.orthographicSize;
			float _moveSpd = orthoSize * _moveSpdOnOrthoSize;

            Key2Move(_moveSpd);
		}

		private void Key2Move(float _moveSpd)
        {
            if (Input.GetKey(_UpKey))
            {
                Move( _moveSpd * Vector2.up);
            }
            if (Input.GetKey(_DownKey))
            {
                Move( _moveSpd * Vector2.down);
            }
            if (Input.GetKey(_RightKey))
            {
                Move( _moveSpd * Vector2.right);
            }
            if (Input.GetKey(_LeftKey))
            {
                Move( _moveSpd * Vector2.left);
            }
        }

        public void Move(Vector2 dir)
		{
            _moveVel += dir;
		}

        public void StopMove()
        {
            _moveVel = Vector2.zero;
        }

		void ZoomCamera (float zoomDelta)
		{
			Camera cam = _MainCam;
			if (cam == null) {
				cam = Camera.main;
			}
			cam.orthographic = true;
			cam.orthographicSize += zoomDelta;
			cam.orthographicSize = Mathf.Clamp (
				cam.orthographicSize, _ZoomMin, _ZoomMax);
		}

		public void StartZoomingIn()
		{
			_zoom = -1.0f;
		}

		public void StopZoomingIn()
		{
			_zoom = 0.0f;
		}

		public void StartZoomingOut()
		{
			_zoom = 1.0f;
		}

		public void StopZoomingOut()
		{
			_zoom = 0.0f;
		}


		public void Pan(Vector2 panAmount)
		{
			//panAmount.x = -panAmount.x;
			Vector2 panMove = _panSpdOnOrthoSize * panAmount;
            _moveVel += panMove;
		}

        public void PanUp(float dist)
        {
            Pan(dist*Vector2.up);
        }

        public void PanDown(float dist)
        {
            Pan(dist *Vector2.down);
        }

        public void PanLeft(float dist)
        {
            Pan(dist *Vector2.left);
        }

        public void PanRight(float dist)
        {
            Pan(dist *Vector2.right);
        }

	}
}

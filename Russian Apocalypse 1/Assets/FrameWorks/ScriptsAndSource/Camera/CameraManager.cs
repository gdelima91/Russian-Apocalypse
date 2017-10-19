using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this Script on the Camera
/// Put the Camera as a child of the Player root
/// </summary>
namespace Visin1_1
{
    public class CameraManager : MonoBehaviour
    {
        public CameraType cameraType = CameraType.FirstPerson;

        PlayerCamera currentCamera;

        // Use this for initialization
        void Start()
        {


            if (cameraType == CameraType.FirstPerson)
            {
                currentCamera = GetComponent<FirstPersonCamera>();
            }
            else if (cameraType == CameraType.ThridPerson)
            {
                currentCamera = GetComponent<ThridPersonCamera>();
            }
            currentCamera.InitialCamera();
        }

        // Update is called once per frame
        public void UpdateCameraManager()
        {
            currentCamera.UpdateCamera();
        }

        private void LateUpdate()
        {
            currentCamera.LateUpdateCamera();
        }

        public void SetCameraDelta(CameraDelta delta)
        {
            currentCamera.SetCameraDetal(delta);
        }

        public float Yaw()
        {
            return currentCamera.Yaw;
        }

        public struct CameraDelta
        {
            public float delta_yaw;
            public float delta_pitch;
            public float delta_dstToTarget;

        }
    }
}

using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooting
{
    public class CameraController : MonoBehaviour
    {
        enum CamType { Drive, Shot, Top, Back }
        [SerializeField] CamType cam;
        [SerializeField] CinemachineVirtualCamera[] virtualCameras;

        void OnCamChange(InputValue inputValue)
        {
            if (Convert.ToInt32(inputValue.Get()) > 0)
            {
                virtualCameras[(int)cam].Priority = 0;
                if (cam == CamType.Back)
                    cam = CamType.Drive;
                else
                    cam++;
                virtualCameras[(int)cam].Priority = 1;
            }
            else if (Convert.ToInt32(inputValue.Get()) < 0)
            {
                virtualCameras[(int)cam].Priority = 0;
                if (cam == CamType.Drive)
                    cam = CamType.Back;
                else
                    cam--;
                virtualCameras[(int)cam].Priority = 1;
            }
        }

        void OnDriveCam()
        {
            virtualCameras[(int)cam].Priority = 0;
            cam = CamType.Drive;
            virtualCameras[(int)cam].Priority = 1;
        }

        void OnShotCam()
        {
            virtualCameras[(int)cam].Priority = 0;
            cam = CamType.Shot;
            virtualCameras[(int)cam].Priority = 1;
        }

        void OnTopCam()
        {
            virtualCameras[(int)cam].Priority = 0;
            cam = CamType.Top;
            virtualCameras[(int)cam].Priority = 1;
        }

        void OnBackCam()
        {
            virtualCameras[(int)cam].Priority = 0;
            cam = CamType.Back;
            virtualCameras[(int)cam].Priority = 1;
        }
    }
}

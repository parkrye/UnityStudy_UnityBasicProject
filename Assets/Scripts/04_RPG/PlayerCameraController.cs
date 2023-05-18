using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera[] cameras;
        [SerializeField] int camNum;

        void OnCamChange(InputValue inputValue)
        {
            if (Convert.ToInt32(inputValue.Get()) > 0)
            {
                cameras[camNum].Priority = 0;
                if (camNum == 2) camNum = 0;
                else camNum++;
                cameras[camNum].Priority = 1;
            }
            else if (Convert.ToInt32(inputValue.Get()) < 0)
            {
                cameras[camNum].Priority = 0;
                if (camNum == 0) camNum = 2;
                else camNum--;
                cameras[camNum].Priority = 1;
            }
        }
    }

}
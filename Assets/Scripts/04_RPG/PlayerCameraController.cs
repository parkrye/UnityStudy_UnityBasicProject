using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera[] cameras;
        [SerializeField] int camNum;
        [SerializeField] Transform lookatPoint;

        void OnCamChange(InputValue inputValue)
        {
            Vector2 dir = inputValue.Get<Vector2>();

            if (dir.x > 0)
            {
                cameras[camNum].Priority = 0;
                if (camNum == 2) camNum = 0;
                else camNum++;
                cameras[camNum].Priority = 1;
            }
            else if (dir.x < 0)
            {
                cameras[camNum].Priority = 0;
                if (camNum == 0) camNum = 2;
                else camNum--;
                cameras[camNum].Priority = 1;
            }
        }
    }

}
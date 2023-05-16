using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace homework
{
    public class TankAndCannon : MonoBehaviour
    {
        [SerializeField] Transform cannonTransform;
        [SerializeField] GameObject shellPrefab;
        [SerializeField] AudioSource[] audios;  // 장전음, 발포음
        [SerializeField] bool loaded;
        [SerializeField] CinemachineVirtualCamera cannonCamera;

        void OnFire()
        {
            if(loaded)
            {
                audios[1].Play();
                Instantiate(shellPrefab, cannonTransform);
            }
            else
            {
                audios[0].Play();
            }
            loaded = !loaded;
        }

        void OnShift()
        {
            if(cannonCamera.Priority == 0)
                cannonCamera.Priority = 2;
            else
                cannonCamera.Priority = 0;
        }
    }
}
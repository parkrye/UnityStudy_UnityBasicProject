using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource source;

        public void PlayAudio()
        {
            source.Play();
            Debug.Log("»§!");
        }
    }

}
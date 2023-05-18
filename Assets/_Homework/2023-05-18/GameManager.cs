using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        private void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        [SerializeField] int fireCount;
        public int FireCount { get { return fireCount; } }

        public void CountFireCounter()
        {
            fireCount++;
        }
    }

}
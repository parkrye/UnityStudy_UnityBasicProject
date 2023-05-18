using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class PlayerActionManager : MonoBehaviour
    {
        [SerializeField] UnityEvent<int> Hit;

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "EnemyAttack")
            {
                Hit?.Invoke(1);
            }
        }
    }
}

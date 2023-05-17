using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlatformerGame
{
    public class LifeVessle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (Mathf.Pow(2, other.gameObject.layer) == LayerMask.GetMask("Player"))
            {
                TimeManager.GetTimeManager().AddTime(1);
                Destroy(gameObject);
            }

            if (other.gameObject.name == "DeadZone")
            {
                Destroy(gameObject);
            }
        }
    }

}
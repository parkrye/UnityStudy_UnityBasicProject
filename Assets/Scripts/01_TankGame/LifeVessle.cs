using System.Collections;
using UnityEngine;

namespace TankGame
{

    public class LifeVessle : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SelfDestroy());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                GameManager.GetGameManager().Life++;
                Destroy(gameObject);
            }
        }

        IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }

}
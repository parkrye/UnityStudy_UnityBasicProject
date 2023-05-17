using System.Collections;
using UnityEngine;

namespace PlatformerGame
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] float power;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody.AddForce(transform.forward * power);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (Mathf.Pow(2, collision.gameObject.layer) == LayerMask.GetMask("Ground"))
            {
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

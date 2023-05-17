using UnityEngine;

namespace ShootingGame
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] BulletManager manager;
        [SerializeField] Rigidbody rb;
        [SerializeField] float power;

        // Start is called before the first frame update
        void Awake()
        {
            gameObject.SetActive(false);
        }

        public void SetMove(BulletManager _manager, Transform _transform)
        {
            manager = _manager;
            transform.position = _transform.position;
            transform.rotation = _transform.rotation;
            gameObject.SetActive(true);
            rb.AddForce(transform.forward * power, ForceMode.VelocityChange);
        }

        void OnCollisionEnter(Collision collision)
        {
            manager.BulletDestroyed(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            manager.BulletDestroyed(gameObject);
        }
    }

}
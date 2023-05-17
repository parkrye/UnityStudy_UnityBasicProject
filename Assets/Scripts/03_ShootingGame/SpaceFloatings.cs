using UnityEngine;

namespace ShootingGame
{
    public class SpaceFloatings : MonoBehaviour
    {
        [SerializeField] SpaceFloatingsManager spaceFloatingsManager;

        [SerializeField] int toughness;
        [SerializeField] Rigidbody rb;
        [SerializeField] Vector3 moveDir;
        [SerializeField] float moveSpeed;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            gameObject.SetActive(false);
        }

        public void SetMove(SpaceFloatingsManager _spaceFloatingsManager, int size, int _toughness, Vector3 _initialPosition, Vector3 _moveDir, float _moveSpeed)
        {
            spaceFloatingsManager = _spaceFloatingsManager;
            transform.localScale *= size;
            toughness = _toughness;
            transform.position = _initialPosition;
            moveDir = _moveDir;
            moveSpeed = _moveSpeed;
            gameObject.SetActive(true);
            transform.LookAt(moveDir);
        }

        private void FixedUpdate()
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent.gameObject.name == "OuterStarWalls")
            {
                spaceFloatingsManager.FloatingDestroyed(gameObject);
                gameObject.SetActive(false);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Bullet")
            {
                toughness--;
                if (toughness <= 0)
                {
                    spaceFloatingsManager.FloatingDestroyed(gameObject);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
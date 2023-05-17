using UnityEngine;
using UnityEngine.InputSystem;

namespace Homework
{
    public class BallMovement : MonoBehaviour
    {
        Rigidbody rb;

        [SerializeField] Vector3 dir;

        // Start is called before the first frame update
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        void Move()
        {
            rb.AddForce(dir, ForceMode.Force);
        }

        void OnMove(InputValue inputValue)
        {
            Debug.Log("ball : " + inputValue.Get<Vector2>());
            dir.x = inputValue.Get<Vector2>().x;
            dir.z = inputValue.Get<Vector2>().y;
        }

        void OnFire(InputValue inputValue)
        {
            rb.AddForce(Vector3.up * 200f, ForceMode.Force);
        }
    }

}

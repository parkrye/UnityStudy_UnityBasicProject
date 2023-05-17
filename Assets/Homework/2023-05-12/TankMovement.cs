using UnityEngine;
using UnityEngine.InputSystem;

namespace Homework
{
    public class TankMovement : MonoBehaviour
    {
        [SerializeField] Vector3 dir;

        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(transform.position + transform.up, transform.forward * 5f + transform.up, Color.red);
            Move();
            Turn();
        }

        void Move()
        {
            transform.Translate(transform.forward * dir.z * 0.01f);
        }

        void Turn()
        {
            transform.Rotate(transform.up * dir.x);
        }

        void OnLook(InputValue inputValue)
        {
            dir.x = inputValue.Get<Vector2>().x;
            dir.z = inputValue.Get<Vector2>().y;
        }
    }

}

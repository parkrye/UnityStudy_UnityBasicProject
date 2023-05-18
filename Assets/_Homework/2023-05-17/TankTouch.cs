using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Homework
{
    public class TankTouch : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Rigidbody rb;
        [SerializeField] Vector2 moveDir;

        void Update()
        {
            Move();
        }

        void Move()
        {
            rb.velocity = moveDir.x * transform.right * 10 + moveDir.y * transform.forward * 10;
        }

        public void OnFireButton()
        {
            animator.SetTrigger("Fire");
        }

        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector2>();
        }

    }

}
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class PlayerMovementController : MonoBehaviour
    {
        Animator animator;
        Rigidbody rb;
        [SerializeField] Vector2 moveDir;
        [SerializeField] float moveSpeed, jumpPower, turnSpeed;

        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            IsGround();
            Move();
            Turn();
        }

        void Move()
        {

            if (!animator.GetBool("Crouch") && !animator.GetBool("Guard") && !animator.GetBool("Attack"))
            {
                if (!animator.GetBool("Run"))
                {
                    rb.AddForce((transform.forward * moveDir.y) * moveSpeed, ForceMode.Force);
                }
                else
                {
                    rb.AddForce((transform.forward * moveDir.y) * moveSpeed * 1.5f, ForceMode.Force);
                }
            }
        }

        void Turn()
        {
            transform.Rotate(transform.up * moveDir.x * turnSpeed);
        }

        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector2>();

            if (moveDir == Vector2.zero)
            {
                animator.SetBool("Move", false);
            }
            else
            {
                animator.SetBool("Move", true);

                if (moveDir.y > 0)
                {
                    animator.SetBool("Foward", true);
                }
                else
                {
                    animator.SetBool("Foward", false);
                }
            }
        }

        void OnRun()
        {
            animator.SetBool("Run", !animator.GetBool("Run"));
        }

        void OnJump()
        {
            if (animator.GetBool("IsGround"))
            {
                animator.SetTrigger("Jump");
                rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
        }

        void OnCrouch()
        {
            moveDir = Vector2.zero;
            animator.SetBool("Move", false);
            animator.SetBool("Crouch", !animator.GetBool("Crouch"));
        }

        void IsGround()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + transform.up * 0.5f, Vector3.down, Color.red);
            if (Physics.Raycast(transform.position + transform.up, Vector3.down, out hit, 1.2f, LayerMask.GetMask("Ground")))
            {
                animator.SetBool("IsGround", true);
                return;
            }
            animator.SetBool("IsGround", false);
        }
    }

}
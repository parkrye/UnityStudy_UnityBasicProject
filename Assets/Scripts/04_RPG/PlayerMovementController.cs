using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG
{
    public class PlayerMovementController : MonoBehaviour
    {
        Animator animator;
        Rigidbody rb;
        [SerializeField] Vector2 moveDir;
        [SerializeField] Vector2 nowMousePos, prevMousePos;
        [SerializeField] float moveSpeed, jumpPower, turnXSpeed, turnYSpeed;
        [SerializeField] Transform lookatPoint;

        // Start is called before the first frame update
        void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            IsGround();
            Move();
        }

        void LateUpdate()
        {
            if (lookatPoint.localPosition.y < 0f)
            {
                lookatPoint.localPosition = new Vector3(0f, 0f, 1f);
            }
            if (lookatPoint.localPosition.y > 3f)
            {
                lookatPoint.localPosition = new Vector3(0f, 3f, 1f);
            }
        }

        void Move()
        {
            if (!animator.GetBool("Crouch") && !animator.GetBool("Guard") && !animator.GetBool("Attack"))
            {
                if (!animator.GetBool("Run"))
                {
                    rb.AddForce((transform.forward * moveDir.y + transform.right * moveDir.x) * moveSpeed, ForceMode.Force);
                }
                else
                {
                    rb.AddForce((transform.forward * moveDir.y + transform.right * moveDir.x) * moveSpeed * 1.5f, ForceMode.Force);
                }
            }
        }

        void OnMouseMove(InputValue inputValue)
        {
            nowMousePos = inputValue.Get<Vector2>();

            if(prevMousePos != Vector2.zero)
            {
                if(lookatPoint.localPosition.y >= 0f && lookatPoint.localPosition.y <= 3f)
                {
                    lookatPoint.Translate(Vector3.up * (prevMousePos.y - nowMousePos.y) * turnYSpeed * Time.deltaTime);
                }
                transform.Rotate(Vector3.up * (prevMousePos.x - nowMousePos.x) * turnXSpeed * Time.deltaTime);
            }

            prevMousePos = nowMousePos;
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
using System.Collections;
using System.Collections.Generic;
using TankGameScripts;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PlatformerGame
{
    public class PlayerContoller : MonoBehaviour
    {
        [SerializeField] CameraController cameraController;
        [SerializeField] Transform cameraTransform;
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] Animator animator;

        [SerializeField] [Range (1, 10)] float movePower, jumpPower;

        [SerializeField] float moveDir;       // 이동 방향
        [SerializeField] float prevDir;       // 이전 이동 방향
        [SerializeField] bool isGrounded;

        // Update is called once per frame
        void Update()
        {
            IsGround();
            Move();
        }

        void Move()
        {
            rigidbody.AddForce(cameraTransform.right * moveDir * movePower);
        }

        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector2>().x;

            if(moveDir > 0) transform.LookAt(transform.position + cameraTransform.right.normalized);
            else transform.LookAt(transform.position - cameraTransform.right.normalized);

            if(moveDir != 0 && !animator.GetBool("Run"))
                animator.SetBool("Run", true);
            else if (moveDir == 0 && animator.GetBool("Run"))
                animator.SetBool("Run", false);
        }

        void OnJump(InputValue inputValue)
        {
            if (isGrounded)
            {
                animator.SetTrigger("Jump");
                animator.SetBool("Fall", true);
                rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            }
        }

        void OnTurn(InputValue inputValue)
        {
            if (inputValue.Get<Vector2>().x > 0)
            {
                transform.Rotate(0f, 90f, 0f);
                cameraController.CameraTurn(1);
            }
            else if (inputValue.Get<Vector2>().x < 0)
            {
                transform.Rotate(0f, -90f, 0f);
                cameraController.CameraTurn(-1);
            }

        }

        void IsGround()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + transform.up, Vector3.down, Color.red);
            Debug.DrawRay(transform.position + transform.up + transform.forward * 0.2f, Vector3.down, Color.red);
            Debug.DrawRay(transform.position + transform.up - transform.forward * 0.2f, Vector3.down, Color.red);
            Debug.DrawRay(transform.position + transform.up + transform.right * 0.2f, Vector3.down, Color.red);
            Debug.DrawRay(transform.position + transform.up - transform.right * 0.2f, Vector3.down, Color.red);
            if (Physics.Raycast(transform.position + transform.up, Vector3.down, out hit, 1.2f, LayerMask.GetMask("Ground")) ||
                Physics.Raycast(transform.position + transform.up + transform.forward * 0.2f, Vector3.down, out hit, 1.2f, LayerMask.GetMask("Ground")) ||
                Physics.Raycast(transform.position + transform.up - transform.forward * 0.2f, Vector3.down, out hit, 1.2f, LayerMask.GetMask("Ground")) ||
                Physics.Raycast(transform.position + transform.up + transform.right * 0.2f, Vector3.down, out hit, 1.2f, LayerMask.GetMask("Ground")) ||
                Physics.Raycast(transform.position + transform.up - transform.right * 0.2f, Vector3.down, out hit, 1.2f, LayerMask.GetMask("Ground")))
            {
                isGrounded = true;

                animator.SetBool("Fall", false);
                return;
            }
            isGrounded = false;
            animator.SetBool("Fall", true);
        }
    }
}
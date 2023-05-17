using System.Collections;
using TankGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PlatformerGame
{
    public class PlayerContoller : MonoBehaviour
    {
        static PlayerContoller playerController;

        void Awake()
        {
            if (playerController == null)
                playerController = this;
        }

        public static PlayerContoller GetPlayerContoller()
        {
            return playerController;
        }

        [SerializeField] CameraController cameraController;
        [SerializeField] Transform cameraTransform;
        [SerializeField] Transform shotTransform;
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] new CapsuleCollider collider;
        [SerializeField] Animator animator;
        [SerializeField] GameObject bulletPrefab;

        [SerializeField] [Range (1, 10)] float movePower, jumpPower;

        [SerializeField] float moveDir;       // 이동 방향
        [SerializeField] float prevDir;       // 이전 이동 방향
        [SerializeField] bool isGrounded, isSit;

        // Update is called once per frame
        void Update()
        {
            IsGround();
            Move();
        }

        void Move()
        {
            rigidbody.AddForce(cameraTransform.right * moveDir * movePower, ForceMode.Acceleration);
        }

        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector2>().x;

            if(moveDir == 0)
            {
                if (moveDir == 0 && animator.GetBool("Run"))
                    animator.SetBool("Run", false);
            }
            else
            {
                cameraController.CameraTurn((int)moveDir);

                if (moveDir > 0) transform.LookAt(transform.position + cameraTransform.right.normalized);
                else transform.LookAt(transform.position - cameraTransform.right.normalized);

                if (!animator.GetBool("Run"))
                    animator.SetBool("Run", true);
            }
        }

        void OnJump()
        {
            if (isGrounded)
            {
                animator.SetTrigger("Jump");
                animator.SetBool("Fall", true);
                rigidbody.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
            }
        }

        void OnSit()
        {
            if (isGrounded && !isSit)
            {
                collider.height = 0.9f;
                collider.center = new Vector3(0, 0.4f, 0);
                isSit = true;
                animator.SetTrigger("Sit");
                if (animator.GetBool("Run"))
                {
                    rigidbody.AddForce(cameraTransform.right * moveDir * movePower * 2f, ForceMode.VelocityChange);
                }
            }
        }

        public void OnSitOver()
        {
            collider.height = 1.8f;
            collider.center = new Vector3(0, 0.8f, 0);
            isSit = false;
        }

        void OnAttack()
        {
            if(isGrounded && !isSit)
            {
                animator.SetTrigger("Attack");
            }
        }

        public void OnBulletShot()
        {
            GameObject bullet = Instantiate(bulletPrefab, shotTransform.position, shotTransform.rotation);
        }

        public void GameClear()
        {
            StartCoroutine(GameClearCoroutine());
        }

        IEnumerator GameClearCoroutine()
        {
            animator.SetTrigger("Clear");
            yield return new WaitForSeconds(3f);
            UIManager.GetUIManager().SetScreen(4);
        }

        void OnTurn(InputValue inputValue)
        {
            if (inputValue.Get<Vector2>().x > 0)
            {
                StartCoroutine(TurnCoroutine(-1));
            }
            else if (inputValue.Get<Vector2>().x < 0)
            {
                StartCoroutine(TurnCoroutine(1));
            }
        }

        IEnumerator TurnCoroutine(int dir)
        {
            for(int i = 0; i < 90; i++)
            {
                transform.Rotate(0f, dir, 0f);
                yield return new WaitForSeconds(0.0001f);
            }
        }

        void OnESC()
        {
            UIManager.GetUIManager().SetScreen(1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == "DeadZone")
            {
                UIManager.GetUIManager().SetScreen(2);
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
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TankGame
{
    public class TankController : MonoBehaviour, IGameSubject
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] Transform turretTransform;
        [SerializeField] AudioSource[] audios;  // 운행, 평상, 폭발

        [SerializeField][Range(1, 5)] float power;

        List<IGameObserver> observers;

        [SerializeField] Vector2 moveDir, turnDir;

        private void Awake()
        {
            observers = new List<IGameObserver>();
        }

        private void Update()
        {
            Move();
            Turn();
        }

        void Move()
        {
            rigidbody.velocity = moveDir.y * power * transform.forward;
            if (moveDir.y != 0f)
                transform.localEulerAngles += moveDir.x / 10 * transform.up;
        }

        void Turn()
        {
            if (turnDir.x != 0)
                turretTransform.localEulerAngles += new Vector3(0, turnDir.x / 10, 0);
            if (turnDir.y != 0)
            {
                Vector3 angle = turretTransform.eulerAngles + new Vector3(-turnDir.y / 10, 0, 0);
                angle.x = (angle.x > 180f) ? angle.x - 360f : angle.x;
                angle.x = Mathf.Clamp(angle.x, -30f, 20f);
                turretTransform.rotation = Quaternion.Euler(angle);
            }
        }

        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector2>();
            if (moveDir == Vector2.zero)
            {
                audios[0].Stop();
                audios[1].Play();
            }
            else
            {
                audios[0].Play();
                audios[1].Stop();
            }
        }

        void OnLook(InputValue inputValue)
        {
            turnDir = inputValue.Get<Vector2>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Shell")
            {
                GameManager.GetGameManager().Life--;
                if(GameManager.GetGameManager().Life == 0)
                {
                    audios[2].Play();
                }
                SendObserver();
            }
        }

        public void AddObserver(IGameObserver shotObserver)
        {
            observers.Add(shotObserver);
        }

        public void RemoveObserver(IGameObserver shotObserver)
        {
            observers.Remove(shotObserver);
        }

        public void SendObserver()
        {
            foreach (IGameObserver observer in observers)
            {
                observer.ReceiveSubject();
            }
        }
    }
}


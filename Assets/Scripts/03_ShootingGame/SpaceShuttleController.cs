using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShootingGame
{
    public class SpaceShuttleController : MonoBehaviour
    {
        [SerializeField] ResourceManager resource;
        [SerializeField] Rigidbody rb;
        [SerializeField] ParticleSystem particle;
        [SerializeField] Transform shotTransform;
        [SerializeField] BulletManager bullet;

        [SerializeField] Vector3 moveDir;
        [SerializeField] Vector2 rotateDir;

        [SerializeField] float movePower, rotatePower, boostPower;
        [SerializeField] bool useEngineMove, useEngineRotate, useEngineBoost;

        void FixedUpdate()
        {
            Move();
            Rotate();
        }

        void LateUpdate()
        {
            if((useEngineMove || useEngineRotate || useEngineBoost))
            {
                resource.UseEngine();
                if (!particle.isPlaying)
                    particle.Play();
            }
            else
            {
                if(particle.isPlaying)
                    particle.Stop();
            }
        }

        void Move()
        {
            rb.AddForce((transform.right * moveDir.x + transform.up * moveDir.y + transform.forward * moveDir.z) * movePower, ForceMode.Acceleration);
        }
        
        void Rotate()
        {
            transform.Rotate(rotateDir * rotatePower);
        }

        // 이동
        void OnMove(InputValue inputValue)
        {
            moveDir = inputValue.Get<Vector3>();
            if(moveDir == Vector3.zero)
            {
                useEngineMove = false;
            }
            else
            {
                useEngineMove = true;
            }
        }

        // 방향전환
        void OnRotate(InputValue inputValue)
        {
            rotateDir = new Vector2(-inputValue.Get<Vector2>().y, inputValue.Get<Vector2>().x);
            if (rotateDir == Vector2.zero)
            {
                useEngineRotate = false;
            }
            else
            {
                useEngineRotate = true;
            }
        }

        // 부스트
        void OnBoost(InputValue inputValue)
        {
            if (Convert.ToInt16(inputValue.Get<float>()) == 0f)
            {
                useEngineBoost = false;
            }
            else
            {
                useEngineBoost = true;
            }
            rb.AddForce(transform.forward * Convert.ToInt16(inputValue.Get<float>()) * boostPower, ForceMode.VelocityChange);
        }

        void OnFire()
        {
            resource.UseEnergy();
            bullet.CreateBullet(shotTransform);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerGame
{
    public class SandBlock : MonoBehaviour, IBlock
    {
        [SerializeField] new Rigidbody rigidbody;

        ILevel level;
        [SerializeField] Vector3 position;

        void Awake()
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        void Start()
        {
            position = transform.position;
        }

        void LateUpdate()
        {
            if(Vector3.Distance(transform.position, position) > 10f)
            {
                SendMsg();
                Destroy(gameObject);
            }
        }

        public void AddLevel(ILevel _level)
        {
            level = _level;
        }

        public void SendMsg()
        {
            level.ReceiveMsg(position);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (Mathf.Pow(2, collision.gameObject.layer) == LayerMask.GetMask("Player"))
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }

            if (Mathf.Pow(2, collision.gameObject.layer) == LayerMask.GetMask("Bullet"))
            {
                Destroy(collision.gameObject);
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

}
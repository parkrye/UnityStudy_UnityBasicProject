using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace homework
{

    public class Jumping : MonoBehaviour
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] float jumpPower;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
    }

}
using UnityEngine;

namespace Homework
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
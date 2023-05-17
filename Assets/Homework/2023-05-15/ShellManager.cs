using System.Collections;
using UnityEngine;

namespace Homework
{
    public class ShellManager : MonoBehaviour
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] new ParticleSystem particleSystem;

        [SerializeField][Range(800, 1200)] float power;

        [SerializeField] Vector3 prevPosition;

        private void Start()
        {
            transform.parent = GameObject.Find("Shells").transform;
            prevPosition = transform.position;
            rigidbody.AddForce(transform.forward * power, ForceMode.Force);
            StartCoroutine(SelfDestroyer(10f));
        }

        void Update()
        {
            transform.LookAt(transform.position * 2 - prevPosition);
            prevPosition = transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            particleSystem.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(SelfDestroyer(1f));
        }

        IEnumerator SelfDestroyer(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}


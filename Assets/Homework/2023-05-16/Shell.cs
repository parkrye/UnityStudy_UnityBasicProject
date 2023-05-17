using System.Collections;
using UnityEngine;

namespace Homework
{
    public class Shell : MonoBehaviour
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] new AudioSource audio;
        [SerializeField] ParticleSystem particle;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody.AddForce(transform.forward * 1000f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            particle.Play();
            audio.Play();
            StartCoroutine(SelfDestroy());
        }

        IEnumerator SelfDestroy()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }

}
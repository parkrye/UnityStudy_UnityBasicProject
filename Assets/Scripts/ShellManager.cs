using System.Collections;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] new ParticleSystem particleSystem;

    [SerializeField] float power;

    private void Start()
    {
        rigidbody.AddForce(transform.forward * power, ForceMode.Force);
        StartCoroutine(SelfDestroyer(10f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        particleSystem.Play();
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(SelfDestroyer(1f));
    }

    IEnumerator SelfDestroyer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

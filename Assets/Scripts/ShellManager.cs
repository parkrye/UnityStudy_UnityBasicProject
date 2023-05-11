using System.Collections;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    new Rigidbody rigidbody;

    [SerializeField] float power;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * power, ForceMode.Force);
        StartCoroutine(SelfDestroyer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    IEnumerator SelfDestroyer()
    {
        yield return new WaitForSecondsRealtime(10f);
        Destroy(gameObject);
    }
}

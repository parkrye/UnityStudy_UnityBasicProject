using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform cannonTransform;
    public GameObject shellPrefab;
    ParticleSystem shellParticle;

    [SerializeField] float power;
    [SerializeField] float coolTime;
    [SerializeField] bool isReady;

    void Start()
    {
        isReady = true;
        shellParticle = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(CoolTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady && Input.GetKeyUp(KeyCode.Space))
        {
            shellParticle.Play();
            Instantiate(shellPrefab, cannonTransform.position, cannonTransform.rotation);
            isReady = false;
        }
    }

    IEnumerator CoolTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(coolTime);
            isReady = true;
        }
    }
}

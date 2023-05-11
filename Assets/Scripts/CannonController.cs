using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CannonController : MonoBehaviour
{
    [SerializeField] Transform cannonTransform;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] ParticleSystem shellParticle;

    [SerializeField] float coolTime;

    private void Start()
    {
        StartCoroutine(CoolTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetGameManager().Ready && Input.GetKeyUp(KeyCode.Space))
        {
            shellParticle.Play();
            Instantiate(shellPrefab, cannonTransform.position, cannonTransform.rotation);
            GameManager.GetGameManager().Ready = false;
        }
    }

    IEnumerator CoolTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolTime);
            GameManager.GetGameManager().Ready = true;
        }
    }
}

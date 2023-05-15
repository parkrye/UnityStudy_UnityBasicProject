using System.Collections;
using UnityEngine;

namespace homework
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField] Transform cannonTransform;
        [SerializeField] GameObject shellPrefab;
        [SerializeField] ParticleSystem shellParticle;
        [SerializeField] bool shot;

        private void Start()
        {
            StartCoroutine(ContinuousFire());
        }

        void OnFire()
        {
            shot = !shot;
        }

        IEnumerator ContinuousFire()
        {
            while (true)
            {
                if(shot)
                {
                    shellParticle.Play();
                    Instantiate(shellPrefab, cannonTransform.position, cannonTransform.rotation);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

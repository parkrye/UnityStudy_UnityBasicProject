using UnityEngine;

namespace ShootingGame
{
    public class AroundPlayer : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = playerTransform.position;
        }
    }

}
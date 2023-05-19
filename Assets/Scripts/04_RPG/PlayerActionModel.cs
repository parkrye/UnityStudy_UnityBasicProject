using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class PlayerActionModel : MonoBehaviour
    {
        [SerializeField] UnityEvent<int> Hit;

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "EnemyAttack")
            {
                Hit?.Invoke(-1);
            }
        }
    }
}

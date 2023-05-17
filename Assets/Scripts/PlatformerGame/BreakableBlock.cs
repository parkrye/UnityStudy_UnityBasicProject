using System.Collections;
using UnityEngine;

namespace PlatformerGame
{
    public class BreakableBlock : MonoBehaviour, IBlock
    {
        ILevel level;
        [SerializeField] Vector3 position;

        void Start()
        {
            position = transform.position;
        }

        public void AddLevel(ILevel _level)
        {
            level = _level;
        }

        public void SendMsg()
        {
            level.ReceiveMsg(position);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (Mathf.Pow(2, collision.gameObject.layer) == LayerMask.GetMask("Bullet"))
            {
                StartCoroutine(SelfBreak());
            }
        }

        IEnumerator SelfBreak()
        {
            for(int i = 0; i < 100; i++)
            {
                transform.localScale *= 0.9f;
                yield return new WaitForSeconds(0.01f);
            }
            SendMsg();
            Destroy(gameObject);
        }
    }

}
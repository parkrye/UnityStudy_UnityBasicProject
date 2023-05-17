using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class BulletManager : MonoBehaviour
    {
        List<GameObject> bullets;
        [SerializeField] GameObject bulletPrefab;

        void Awake()
        {
            bullets = new List<GameObject>();
            for (int i = 0; i < 100; i++)
            {
                GameObject tmp = Instantiate(bulletPrefab);
                tmp.transform.parent = transform;
                bullets.Add(tmp);
            }
        }

        public void CreateBullet(Transform shotTransform)
        {
            if(bullets.Count == 0)
            {
                GameObject tmp = Instantiate(bulletPrefab);
                tmp.transform.parent = transform;
                bullets.Add(tmp);
            }

            GameObject bullet = bullets[0];
            bullet.GetComponent<Bullet>().SetMove(this, shotTransform);
            bullets.Remove(bullet);
        }

        public void BulletDestroyed(GameObject bullet)
        {
            bullets.Add(bullet);
        }
    }

}
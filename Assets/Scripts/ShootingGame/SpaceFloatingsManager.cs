using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class SpaceFloatingsManager : MonoBehaviour
    {
        List<GameObject> spaceFloatings;
        [SerializeField] GameObject[] floatingPrefabs;
        [SerializeField] Transform playerTransform, outerTransform;
        [SerializeField] ResourceManager manager;

        void Awake()
        {
            spaceFloatings = new List<GameObject>();
            foreach (GameObject floating in floatingPrefabs)
            {
                GameObject tmp = Instantiate(floating);
                tmp.transform.parent = transform;
                spaceFloatings.Add(tmp);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnFloating());
        }

        IEnumerator SpawnFloating()
        {
            while (true)
            {
                if(spaceFloatings.Count == 0)
                {
                    foreach (GameObject f in floatingPrefabs)
                    {
                        GameObject tmp = Instantiate(f);
                        tmp.transform.parent = transform;
                        spaceFloatings.Add(tmp);
                    }
                }

                GameObject floating = spaceFloatings[Random.Range(0, spaceFloatings.Count)];
                spaceFloatings.Remove(floating);
                int randomSize = Random.Range(10, 100);
                int randomToughness = Random.Range(10, 20);
                int randomSpeed = Random.Range(1, 5);

                float randomX = Random.Range(-Vector3.Distance(playerTransform.position, outerTransform.position) * 0.75f, Vector3.Distance(playerTransform.position, outerTransform.position) * 0.75f);
                float randomY = Random.Range(-Vector3.Distance(playerTransform.position, outerTransform.position) * 0.75f, Vector3.Distance(playerTransform.position, outerTransform.position) * 0.75f);
                float randomZ = Random.Range(-Vector3.Distance(playerTransform.position, outerTransform.position) * 0.75f, Vector3.Distance(playerTransform.position, outerTransform.position) * 0.75f);

                floating.GetComponent<SpaceFloatings>().SetMove(this, randomSize, randomToughness, new Vector3(randomX, randomY, randomZ), playerTransform.position, randomSpeed);
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void FloatingDestroyed(GameObject floating)
        {
            manager.FillEngine(1000);
            manager.FillEnergy(10);
            manager.AddScore();
            spaceFloatings.Add(floating);
        }
    }
}
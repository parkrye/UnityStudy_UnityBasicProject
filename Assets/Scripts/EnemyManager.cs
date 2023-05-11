using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrafab;
    public BoxCollider spawnRangeCollider;

    [SerializeField] const int enemyLimit = 10;
    [SerializeField] int enemyCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if(enemyCount < enemyLimit)
            {
                Instantiate(enemyPrafab, Return_RandomPosition(), Quaternion.identity);
            }
            yield return new WaitForSecondsRealtime(Random.Range(10, 30));
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = spawnRangeCollider.transform.position;

        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = spawnRangeCollider.bounds.size.x;
        float range_Z = spawnRangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    static EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = this;
    }

    public static EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    [SerializeField] GameObject enemyPrafab;
    [SerializeField] BoxCollider spawnRangeCollider;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] int enemyLimit = 10;
    [SerializeField] int enemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void BreakEnemy()
    {
        enemyLimit++;
        enemyCount--;
        GameManager.GetGameManager().Score++;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if(enemyCount < enemyLimit)
            {
                Vector3 spawnPos = Return_RandomPosition();
                while (!agent.CalculatePath(spawnPos, new NavMeshPath()))
                    spawnPos = Return_RandomPosition();
                Vector3 movePos = Return_RandomPosition();
                while (Vector3.Distance(spawnPos, movePos) < 50 || !agent.CalculatePath(movePos, new NavMeshPath()))
                    movePos = Return_RandomPosition();

                GameObject enemy = Instantiate(enemyPrafab, spawnPos, Quaternion.identity);
                enemy.GetComponent<EnemyController>().SetMoveRoute(spawnPos, movePos);
                enemyCount++;
            }
            yield return new WaitForSeconds(Random.Range(10, 30));
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

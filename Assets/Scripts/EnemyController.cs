using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class EnemyController : MonoBehaviour
{
    public Transform cannonTransform;
    public GameObject shellPrefab;
    NavMeshAgent navMeshAgent;
    ParticleSystem shellParticle;

    [SerializeField] float power;
    [SerializeField] float coolTime;
    [SerializeField] bool isReady;

    [SerializeField] private float viewAngle;       // 시야 각도
    [SerializeField] private float viewDistance;    // 시야 거리
    [SerializeField] private LayerMask targetMask;  // 타겟 마스크

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        shellParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCheck())
        {
            MoveAround();
        }
        else
        {
            AttackPlayer();
        }
    }

    /// <summary>
    /// 각도를 방향 벡터로 변경하는 메소드
    /// </summary>
    /// <param name="angle">각도</param>
    /// <returns>방향 벡터</returns>
    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    /// <summary>
    /// 플레이어를 확인하는 시야 메소드
    /// </summary>
    /// <returns>플레이어 발견 여부</returns>
    bool PlayerCheck()
    {

        Debug.DrawRay(transform.position, AngleToDir(viewAngle * 0.5f) * viewDistance, Color.blue);
        Debug.DrawRay(transform.position, AngleToDir(viewAngle * -0.5f) * viewDistance, Color.blue);
        Debug.DrawRay(transform.position, AngleToDir(viewAngle) * viewDistance, Color.cyan);

        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, targetMask);
        foreach (Collider target in targets)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(AngleToDir(viewAngle), targetDir)) * Mathf.Rad2Deg;
            if(targetAngle <= viewAngle * 0.5f && !Physics.Raycast(transform.position, targetDir, viewDistance, targetMask))
            {
                return true;
            }
        }
        return false;
    }

    void MoveAround()
    {

    }

    void AttackPlayer()
    {
        Debug.Log("Find");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;


namespace TankGameScripts
{
    public class EnemyController : MonoBehaviour, IGameSubject
    {
        [SerializeField] Transform cannonTransform;
        [SerializeField] GameObject shellPrefab;
        [SerializeField] GameObject lifeVesslePrefab;
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] ParticleSystem tankParticle, shellParticle;

        [SerializeField][Range(60f, 120f)] float viewAngle;
        [SerializeField][Range(20f, 60f)] float viewDistance;
        [SerializeField] LayerMask targetMask;
        [SerializeField] bool live = true;

        public IObjectPool<GameObject> pool;

        enum Mode { Noramal, Battle }
        [SerializeField] Mode mode = Mode.Noramal;

        [SerializeField] Vector3 startPoint, endPoint, targetPosition;
        List<IGameObserver> observers;

        private void Awake()
        {
            observers = new List<IGameObserver>();
        }

        public void SetMoveRoute(Vector3 pont1, Vector3 point2)
        {
            startPoint = pont1;
            endPoint = point2;
            navMeshAgent.SetDestination(endPoint);
        }

        // Update is called once per frame
        void Update()
        {
            if (endPoint != null && live)
            {
                PlayerCheck();
                Move();
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
        void PlayerCheck()
        {
            Debug.DrawRay(transform.position, AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f) * viewDistance, Color.blue);
            Debug.DrawRay(transform.position, AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f) * viewDistance, Color.blue);

            Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, targetMask);
            foreach (Collider target in targets)
            {
                Vector3 targetPos = target.transform.position;
                Vector3 targetDir = (targetPos - transform.position).normalized;
                float targetAngle = Vector3.Angle(targetDir, transform.forward);
                if (targetAngle <= viewAngle * 0.5f && Physics.Raycast(transform.position, targetDir, viewDistance, targetMask))
                {
                    Debug.DrawLine(transform.position, targetPos, Color.red);
                    targetPosition = targetPos;
                    if (mode == Mode.Noramal)
                    {
                        mode = Mode.Battle;
                        StartCoroutine(AttackPlayer());
                    }
                    return;
                }
            }

            if (mode == Mode.Battle)
            {
                StopCoroutine(AttackPlayer());
                navMeshAgent.SetDestination(endPoint);
                mode = Mode.Noramal;
            }
        }

        void Move()
        {
            if (mode == Mode.Noramal)
            {
                if (Vector3.Distance(transform.position, endPoint) <= 10f)
                {
                    Vector3 temp = endPoint;
                    endPoint = startPoint;
                    startPoint = temp;
                    navMeshAgent.SetDestination(endPoint);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, targetPosition) > 20f)
                {
                    navMeshAgent.SetDestination(targetPosition);
                }
                else
                {
                    navMeshAgent.destination = transform.position;
                    transform.LookAt(targetPosition);
                }
            }
        }

        public void Respawn()
        {
            transform.position = startPoint;
            GetComponent<Collider>().enabled = true;
            foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
                meshRenderer.enabled = true;
            navMeshAgent.SetDestination(endPoint);
        }

        IEnumerator AttackPlayer()
        {
            while (mode == Mode.Battle)
            {
                shellParticle.Play();
                Instantiate(shellPrefab, cannonTransform.position, cannonTransform.rotation);
                yield return new WaitForSeconds(10);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Shell")
            {
                live = false;
                StopCoroutine(AttackPlayer());
                tankParticle.Play();
                GetComponent<Collider>().enabled = false;
                foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
                    meshRenderer.enabled = false;
                StartCoroutine(DestroyThis());
            }
        }

        IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(1f);
            SendObserver();
            if (Random.Range(0, 5) == 0)
                Instantiate(lifeVesslePrefab, transform.position, Quaternion.identity);
            pool.Release(gameObject);
        }

        public void AddObserver(IGameObserver shotObserver)
        {
            observers.Add(shotObserver);
        }

        public void RemoveObserver(IGameObserver shotObserver)
        {
            observers.Remove(shotObserver);
        }

        public void SendObserver()
        {
            foreach (IGameObserver observer in observers)
            {
                observer.ReceiveSubject();
            }
        }
    }

}

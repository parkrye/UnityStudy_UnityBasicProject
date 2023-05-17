using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerGame
{
    public class LevelManager : MonoBehaviour, ILevel
    {
        [SerializeField] Transform playerTransform, deadZoneTransform;
        [SerializeField] int mapSizeX, mapSizeZ;
        [SerializeField] GameObject[] blockPrefabs; // 벽돌, 구름, 광석, 모래
        [SerializeField] GameObject lifeVesslePrefab;

        int[,] map;

        // Start is called before the first frame update
        void Awake()
        {
            map = new int[mapSizeX, mapSizeZ];
            bool[,] visit = new bool[mapSizeX, mapSizeZ];
            Stack<Vector3> tileStack = new Stack<Vector3>();

            tileStack.Push(new Vector3(Random.Range(0, 10), Random.Range(0, 30), Random.Range(0, 10)));

            while (tileStack.Count > 0)
            {
                Vector3 nowTile = tileStack.Pop();

                int x = (int)nowTile.x;
                int y = (int)nowTile.y;
                int z = (int)nowTile.z;
                map[x, z] = y;
                visit[x, z] = true;

                GameObject block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
                block.transform.position = new Vector3(x * 1.2f, map[x, z] % 10, z * 1.2f);
                block.transform.parent = transform;
                if (block.GetComponent<BreakableBlock>())
                    block.GetComponent<BreakableBlock>().AddLevel(this);
                else if (block.GetComponent<SandBlock>())
                    block.GetComponent<SandBlock>().AddLevel(this);

                while (block.transform.position.y < deadZoneTransform.position.y)
                    deadZoneTransform.Translate(Vector3.down * 50f);

                if (x + 1 < mapSizeX && !visit[x + 1, z])
                {
                    visit[x + 1, z] = true;
                    tileStack.Push(new Vector3(x + 1, y + Random.Range(-10, 11), z));
                }
                if (x - 1 >= 0 && !visit[x - 1, z])
                {
                    visit[x - 1, z] = true;
                    tileStack.Push(new Vector3(x - 1, y + Random.Range(-10, 11), z));
                }
                if (z + 1 < mapSizeZ && !visit[x, z + 1])
                {
                    visit[x, z + 1] = true;
                    tileStack.Push(new Vector3(x, y + Random.Range(-10, 11), z + 1));
                }
                if (z - 1 >= 0 && !visit[x, z - 1])
                {
                    visit[x, z - 1] = true;
                    tileStack.Push(new Vector3(x, y + Random.Range(-10, 11), z - 1));
                }
            }

            playerTransform.position = new Vector3(mapSizeX * 0.5f * 1.2f, map[(int)(mapSizeX * 0.5f), (int)(mapSizeZ * 0.5f)] % 10 + 3f, mapSizeZ * 0.5f * 1.2f);
        }

        void Start()
        {
            StartCoroutine(SpawnVessle());
        }

        public void ReceiveMsg(Vector3 position)
        {
            GameObject block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
            block.transform.position = new Vector3(position.x, position.y + Random.Range(-10, 11), position.z);
            block.transform.parent = transform;
        }

        IEnumerator SpawnVessle()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                Instantiate(lifeVesslePrefab, new Vector3(Random.Range(0, mapSizeX * 1.2f), 100f, Random.Range(0, mapSizeX * 1.2f)), Quaternion.identity);
            }
        }
    }
}

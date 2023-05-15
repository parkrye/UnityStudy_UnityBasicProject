using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

namespace PlatformerGame
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] int mapSizeX, mapSizeZ;
        [SerializeField] GameObject[] blockPrefabs;
        [SerializeField] Vector3 start, goal;
        int[,] map;
        bool[,] visit;
        Stack<Vector3> tileStack;

        // Start is called before the first frame update
        void Awake()
        {
            map = new int[mapSizeX, mapSizeZ];
            visit = new bool[mapSizeX, mapSizeZ];
            tileStack = new Stack<Vector3>();

            start = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
            tileStack.Push(start);

            while(tileStack.Count > 0)
            {
                Vector3 nowTile = tileStack.Pop();

                int x = (int)nowTile.x;
                int y = (int)nowTile.y < 0 ? 0 : (int)nowTile.y;
                int z = (int)nowTile.z;
                map[x, z] = y;
                visit[x, z] = true;

                GameObject block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
                block.transform.position = new Vector3(x * 1.2f, map[x, z] % 10, z * 1.2f);
                block.transform.parent = transform;

                if (x + 1 < mapSizeX && !visit[x + 1, z]) tileStack.Push(new Vector3(x + 1, y + Random.Range(-1, 2), z));
                if (x - 1 >= 0 && !visit[x - 1, z]) tileStack.Push(new Vector3(x - 1, y + Random.Range(-1, 2), z));
                if (z + 1 < mapSizeZ && !visit[x, z + 1]) tileStack.Push(new Vector3(x, y + Random.Range(-1, 2), z + 1));
                if (z - 1 >= 0 && !visit[x, z - 1]) tileStack.Push(new Vector3(x, y + Random.Range(-1, 2), z - 1));
            }

            playerTransform.position = new Vector3(start.x * 1.2f, start.y + 1f, start.z * 1.2f);
        }

        void CreateMap()
        {
            start = new Vector3(0, 0, 5);
            goal = new Vector3(9, 9, 9);

            map = new int[10, 10]
            {
                { 5, 1, 1, 4, 5, 6, 2, 3, 4, 3 },
                { 2, 1, 2, 3, 4, 3, 5, 4, 5, 2 },
                { 1, 1, 2, 3, 4, 2, 3, 6, 5, 1 },
                { 3, 2, 1, 1, 2, 1, 6, 5, 4, 3 },
                { 4, 3, 4, 0, 0, 7, 6, 0, 0, 2 },
                { 5, 8, 5, 0, 0, 7, 2, 0, 0, 2 },
                { 6, 7, 5, 6, 7, 7, 1, 1, 1, 2 },
                { 9, 9, 9, 9, 3, 3, 2, 0, 0, 0 },
                { 8, 9, 9, 9, 3, 0, 0, 0, 0, 0 },
                { 7, 6, 5, 4, 3, 4, 5, 6, 7, 5 }
            };

            playerTransform.position = new Vector3(start.x * 1.2f, start.y + 1f, start.z * 1.2f);
        }
    }
}

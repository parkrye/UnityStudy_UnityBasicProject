using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlatformerGame 
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] int cameraDir;
        List<RaycastHit> prevHits;

        void Awake()
        {
            prevHits = new List<RaycastHit>();
        }

        public void CameraTurn(int dir)
        {
            if(dir != 0) cameraDir = dir;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Look();
            LookThrough();
        }

        void Look()
        {
            transform.position = playerTransform.position + playerTransform.up * 2f;
            if (cameraDir == 1) transform.position += playerTransform.right * 5;
            else if(cameraDir == -1) transform.position -= playerTransform.right * 5;
            transform.LookAt(playerTransform.position);
        }

        void LookThrough()
        {
            List<RaycastHit> nowHits = new List<RaycastHit>();
            for(int i = -3; i < 4; i++)
            {
                for(int j = -3; j < 4; j++)
                {
                    Debug.DrawRay(transform.position + Vector3.up.normalized * i / 2f + transform.right.normalized * j / 2f, transform.forward * 4f, Color.green);
                    nowHits.AddRange(Physics.RaycastAll(transform.position + Vector3.up.normalized * i / 2f + transform.right.normalized * j / 2f, transform.forward, 4f, LayerMask.GetMask("Ground")).ToList());
                }
            }

            foreach (RaycastHit hit in prevHits)
            {
                if (!nowHits.Contains(hit))
                {
                    hit.transform.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
            }

            foreach (RaycastHit hit in nowHits)
            {
                hit.transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            }

            prevHits = nowHits;
        }
    }
}



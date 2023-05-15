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
            cameraDir += dir;
            if (cameraDir < 0) cameraDir = 3;
            if(cameraDir > 3) cameraDir = 0;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Look();
            LookThrough();
        }

        void Look()
        {
            transform.position = playerTransform.position + Vector3.up;
            if (cameraDir == 0)
                transform.position += Vector3.back * 5;
            else if(cameraDir == 1)
                transform.position += Vector3.right * 5;
            else if(cameraDir == 2)
                transform.position += Vector3.forward * 5;
            else if(cameraDir == 3)
                transform.position += Vector3.left * 5;
            transform.LookAt(playerTransform.position);
        }

        void LookThrough()
        {
            List<RaycastHit> nowHits = new List<RaycastHit>();
            Debug.DrawRay(transform.position, (playerTransform.position - transform.position) * 4.5f, Color.green);
            nowHits = Physics.RaycastAll(transform.position, transform.forward, 4.5f, LayerMask.GetMask("Ground")).ToList();
            nowHits.AddRange(Physics.RaycastAll(transform.position + playerTransform.up, playerTransform.position + playerTransform.up * 0.5f - transform.position, 4.5f, LayerMask.GetMask("Ground")).ToList());
            nowHits.AddRange(Physics.RaycastAll(transform.position + playerTransform.right, playerTransform.position + playerTransform.up * 0.5f - transform.position - playerTransform.right, 4.5f, LayerMask.GetMask("Ground")).ToList());
            nowHits.AddRange(Physics.RaycastAll(transform.position - playerTransform.right, playerTransform.position + playerTransform.up * 0.5f - transform.position + playerTransform.right, 4.5f, LayerMask.GetMask("Ground")).ToList());

            foreach(RaycastHit hit in nowHits)
            {
                hit.transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            }

            foreach (RaycastHit hit in prevHits)
            {
                if (!nowHits.Contains(hit))
                {
                    hit.transform.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
            }

            prevHits = nowHits;
        }
    }
}



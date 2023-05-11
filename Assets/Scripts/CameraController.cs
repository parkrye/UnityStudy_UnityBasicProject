using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    [SerializeField] float backDistance;
    [SerializeField] float upDistance;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - target.forward * backDistance + Vector3.up * upDistance;
        transform.LookAt(target.position + target.forward * backDistance);
    }
}

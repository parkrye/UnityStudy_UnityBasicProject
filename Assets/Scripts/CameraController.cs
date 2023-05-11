using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float backDistance;
    [SerializeField] float upDistance;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - target.forward * backDistance + Vector3.up * upDistance;
        transform.LookAt(target.position + target.forward * backDistance);
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour, IGameObserver
{
    [SerializeField] Transform target;

    [SerializeField] [Range(1, 5)] float backDistance;
    [SerializeField] [Range(1, 5)] float upDistance;
    [SerializeField] [Range(30, 90)] float readyFOV;
    [SerializeField] [Range(30, 90)] float loadFOV;
    float tmpUpDistance;

    public void ReceiveSubject()
    {
        switch(GameManager.GetGameManager().Shot)
        {
            case GameManager.ShotMode.Shot:
                GetComponent<Camera>().fieldOfView = loadFOV;
                tmpUpDistance = upDistance;
                upDistance = 1f;
                break;
            case GameManager.ShotMode.Reload:
                GetComponent<Camera>().fieldOfView = readyFOV;
                upDistance = tmpUpDistance;
                break;
            case GameManager.ShotMode.Ready:
                break;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - target.forward * backDistance + Vector3.up * upDistance;
        transform.LookAt(target.position + target.forward * backDistance);
    }
}

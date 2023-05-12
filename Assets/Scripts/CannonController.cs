using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CannonController : MonoBehaviour, IGameSubject
{
    [SerializeField] Transform cannonTransform;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] ParticleSystem shellParticle;

    [SerializeField] [Range(1, 5)] float coolTime;

    List<IGameObserver> observers;

    private void Awake()
    {
        observers = new List<IGameObserver>();
    }

    void OnFire()
    {
        if (GameManager.GetGameManager().Shot == GameManager.ShotMode.Ready)
        {
            GameManager.GetGameManager().Shot = GameManager.ShotMode.Shot;
            SendObserver();
        }
        else if (GameManager.GetGameManager().Shot == GameManager.ShotMode.Shot)
        {
            shellParticle.Play();
            Instantiate(shellPrefab, cannonTransform.position, cannonTransform.rotation);
            StartCoroutine(CoolTime());
            GameManager.GetGameManager().Shot = GameManager.ShotMode.Reload;
            SendObserver();
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        GameManager.GetGameManager().Shot = GameManager.ShotMode.Ready;
        SendObserver();
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
        foreach(IGameObserver observer in observers)
        {
            observer.ReceiveSubject();
        }
    }
}

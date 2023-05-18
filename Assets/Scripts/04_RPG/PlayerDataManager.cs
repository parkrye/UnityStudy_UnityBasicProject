using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] int maxHP, nowHP, maxSP, nowSP;

    [SerializeField] UnityEvent DamageEvent, DeadEvent;

    public void OnDamaged(int damage)
    {
        nowHP -= damage;
        if(damage > 0)
            DamageEvent?.Invoke();
        if(nowHP == 0)
            DeadEvent?.Invoke();
    }
}

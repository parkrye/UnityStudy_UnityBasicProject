using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel : MonoBehaviour
{
    [SerializeField] int[] status; // level, maxHP, nowHP, maxSP, nowSP, maxEXP, nowEXP

    [SerializeField] UnityEvent<int[], int> StatusChanged;  // status, 이벤트 번호
                                                            // (0:능력치 변동, 1:체력 감소, 2:체력 회복, 3:기력 감소, 4:기력 회복,
                                                            //  )

    void Awake()
    {
        status = new int[7] { 1, 100, 100, 100, 100, 100, 0 };
        StatusChanged?.Invoke(status, 0);
    }

    /// <summary>
    /// 체력 증감
    /// </summary>
    /// <param name="modifier">양수는 증가, 음수는 감소</param>
    public void OnHPChanged(int modifier)
    {
        if (modifier == 0)
            return;

        if(modifier < 0)
        {
            HPDecrease(modifier);
        }
        else if(modifier > 0)
        {
            HPIncrease(modifier);
        }
        StatusChanged?.Invoke(status, 0);
    }

    void HPDecrease(int damage)
    {
        status[2] += damage;
        if(damage > 0)
            StatusChanged?.Invoke(status, 1);
    }

    void HPIncrease(int heal)
    {
        status[2] += heal;
        if(status[2] > status[1])
            status[2] = status[1];
        StatusChanged?.Invoke(status, 2);
    }

    /// <summary>
    /// 기력 증감
    /// </summary>
    /// <param name="modifier">양수는 증가, 음수는 감소</param>
    public void OnSPChanged(int modifier)
    {
        if (modifier == 0)
            return;

        if(modifier < 0)
        {
            SPDecrease(modifier);
        }
        else if(modifier > 0)
        {
            SPIncrease(modifier);
        }
        StatusChanged?.Invoke(status, 0);
    }

    void SPDecrease(int damage)
    {
        status[4] += damage;
        if(damage > 0)
            StatusChanged?.Invoke(status, 3);
    }

    void SPIncrease(int heal)
    {
        status[4] += heal;
        if(status[4] > status[3])
            status[4] = status[3];
        StatusChanged?.Invoke(status, 4);
    }

    /// <summary>
    /// 경험치 증감
    /// </summary>
    /// <param name="modifier">음수는 반응 없음</param>
    public void OnEXPChanged(int modifier)
    {
        if (modifier <= 0)
            return;

        EXPIncrease(modifier);
        StatusChanged?.Invoke(status, 0);
    }

    void EXPIncrease(int heal)
    {
        status[6] += heal;
        while (status[6] >= status[5])
            LevelUP();
        StatusChanged?.Invoke(status, 4);
    }

    void LevelUP()
    {
        status[0]++;
        status[6] -= status[7];
        status[7] += 10;

        status[1] += 10;
        status[2] = status[1];
        status[3] += 10;
        status[4] = status[3];
    }
}

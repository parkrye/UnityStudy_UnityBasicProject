using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] int time;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        time = 100;
        UIManager.GetUIManager().SetTimer(time);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            timer -= 1f;
            time--;
            UIManager.GetUIManager().SetTimer(time);
            if (time == 0)
                UIManager.GetUIManager().SetScreen(4);
        }
    }
}

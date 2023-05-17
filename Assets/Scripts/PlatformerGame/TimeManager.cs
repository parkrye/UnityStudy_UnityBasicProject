using PlatformerGame;
using UnityEngine;

namespace PlatformerGame
{
    public class TimeManager : MonoBehaviour
    {
        static TimeManager timeManager;

        public static TimeManager GetTimeManager()
        {
            return timeManager;
        }

        void Awake()
        {
            if (timeManager == null)
                timeManager = this;
        }

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
            if (timer >= 1f)
            {
                timer -= 1f;
                if (time == 0)
                {
                    PlayerContoller.GetPlayerContoller().GameClear();
                    Destroy(this);
                }
                else
                    time--;
                UIManager.GetUIManager().SetTimer(time);
            }
        }

        public void AddTime(int add = 1)
        {
            time += add;
        }
    }

}

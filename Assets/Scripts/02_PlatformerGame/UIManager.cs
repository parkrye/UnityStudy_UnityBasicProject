using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PlatformerGame
{
    public class UIManager : MonoBehaviour
    {
        static UIManager uiManager;

        public static UIManager GetUIManager()
        {
            return uiManager;
        }

        [SerializeField] GameObject[] screens;
        [SerializeField] Text timer;

        void Awake()
        {
            if (uiManager == null)
                uiManager = this;
        }

        void Start()
        {
            SetScreen(0);
        }

        public void OnStartButton()
        {
            SetScreen(3);
        }

        public void OnReStartButton()
        {
            SceneManager.LoadScene(2);
        }

        public void OnQuitButton()
        {
            SceneManager.LoadScene(0);
        }

        public void SetTimer(int time)
        {
            timer.text = "남은 시간 : " + time;
        }

        public void SetScreen(int num)
        {
            switch (num)
            {
                case 0:
                    Time.timeScale = 0f;
                    screens[0].SetActive(true);
                    screens[1].SetActive(false);
                    screens[2].SetActive(false);
                    screens[3].SetActive(false);
                    screens[4].SetActive(false);
                    break;
                case 1:
                    Time.timeScale = 0f;
                    screens[0].SetActive(false);
                    screens[1].SetActive(true);
                    screens[2].SetActive(false);
                    screens[3].SetActive(false);
                    screens[4].SetActive(false);
                    break;
                case 2:
                    Time.timeScale = 0f;
                    screens[0].SetActive(false);
                    screens[1].SetActive(false);
                    screens[2].SetActive(true);
                    screens[3].SetActive(false);
                    screens[4].SetActive(false);
                    break;
                case 3:
                    Time.timeScale = 1f;
                    screens[0].SetActive(false);
                    screens[1].SetActive(false);
                    screens[2].SetActive(false);
                    screens[3].SetActive(true);
                    screens[4].SetActive(false);
                    break;
                case 4:
                    Time.timeScale = 0f;
                    screens[0].SetActive(false);
                    screens[1].SetActive(false);
                    screens[2].SetActive(false);
                    screens[3].SetActive(false);
                    screens[4].SetActive(true);
                    break;
            }
        }
    }

}

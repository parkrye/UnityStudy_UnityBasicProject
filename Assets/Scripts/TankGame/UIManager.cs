using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TankGame
{
    public class UIManager : MonoBehaviour, IGameObserver
    {
        static UIManager uiManager;
        enum Screen { InGame, Pause, Over }
        [SerializeField] Screen screen;

        private void Awake()
        {
            uiManager = this;

            Time.timeScale = 0f;
            ScreenSet(0);
        }

        public static UIManager GetUIManager()
        {
            return uiManager;
        }

        [SerializeField] GameObject[] screens = new GameObject[2];
        [SerializeField] Text scoreText, lifeText;
        [SerializeField] Toggle shotToggle;
        [SerializeField] GameObject dotSight;

        public void OnStartButton()
        {
            Time.timeScale = 1.0f;
            UpdateUI();
            ScreenSet(1);
        }
        public void OnQuitButton()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        public void OnRetryButton()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        void UpdateUI()
        {
            scoreText.text = "Score : " + GameManager.GetGameManager().Score;
            lifeText.text = "Life : " + GameManager.GetGameManager().Life;
            shotToggle.interactable = GameManager.GetGameManager().Shot == GameManager.ShotMode.Ready;

            if (GameManager.GetGameManager().Life == 0)
                GameOver();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screen == Screen.InGame)
                {
                    Time.timeScale = 0f;
                    ScreenSet(0);
                }
                else if (screen == Screen.Pause)
                {
                    Time.timeScale = 1.0f;
                    ScreenSet(1);
                }
                else if (screen == Screen.Over)
                {
                    SceneManager.LoadScene(0, LoadSceneMode.Single);
                }
            }
        }

        void ScreenSet(int num)
        {
            switch (num)
            {
                case 0:
                    screen = Screen.Pause;
                    screens[0].SetActive(true);
                    screens[1].SetActive(false);
                    screens[2].SetActive(false);
                    break;
                case 1:
                    screen = Screen.InGame;
                    screens[0].SetActive(false);
                    screens[1].SetActive(true);
                    screens[2].SetActive(false);
                    break;
                case 2:
                    screen = Screen.Over;
                    screens[0].SetActive(false);
                    screens[1].SetActive(false);
                    screens[2].SetActive(true);
                    break;
                default:
                    break;
            }
        }

        void GameOver()
        {
            Time.timeScale = 0f;
            ScreenSet(2);
        }

        public void ReceiveSubject()
        {
            UpdateUI();
            switch (GameManager.GetGameManager().Shot)
            {
                case GameManager.ShotMode.Shot:
                    dotSight.SetActive(true);
                    break;
                case GameManager.ShotMode.Reload:
                    dotSight.SetActive(false);
                    break;
                case GameManager.ShotMode.Ready:
                    break;
            }
        }
    }

}

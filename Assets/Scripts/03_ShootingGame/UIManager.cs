using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject[] screens;

        void Awake()
        {
            Time.timeScale = 0f;
            screens[0].SetActive(true);
            screens[1].SetActive(false);
            screens[2].SetActive(false);
        }

        public void OnStartButton()
        {
            Time.timeScale = 1f;
            screens[0].SetActive(false);
            screens[1].SetActive(true);
            screens[2].SetActive(false);
        }

        public void OnQuitButton()
        {
            SceneManager.LoadScene(0);
        }

        public void OnRestartButton()
        {
            SceneManager.LoadScene(3);
        }

        void OnESC()
        {
            Time.timeScale = 0f;
            screens[0].SetActive(true);
            screens[1].SetActive(false);
            screens[2].SetActive(false);
        }
    }

}
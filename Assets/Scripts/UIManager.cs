using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager uiManager;

    private void Awake()
    {
        uiManager = this;
    }

    public static UIManager GetUIManager()
    {
        return uiManager;
    }

    [SerializeField] GameObject[] screens = new GameObject[2];
    [SerializeField] Text scoreText, lifeText;
    [SerializeField] Toggle shotToggle;

    private void Start()
    {
        Time.timeScale = 0f;
        ScreenSet(0);
    }

    public void OnStartButton()
    {
        Time.timeScale = 1.0f;
        UpdateUI();
        ScreenSet(1);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateUI()
    {
        scoreText.text = "Score : " + GameManager.GetGameManager().Score;
        lifeText.text = "Life : " + GameManager.GetGameManager().Life;
        shotToggle.interactable = GameManager.GetGameManager().Ready;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
                ScreenSet(1);
            }
            else
            {
                Time.timeScale = 1.0f;
                ScreenSet(0);
            }
        }
    }

    void ScreenSet(int num)
    {
        switch (num)
        {
            case 0:
                screens[0].SetActive(true);
                screens[1].SetActive(false);
                screens[2].SetActive(false);
                break;
            case 1:
                screens[0].SetActive(false);
                screens[1].SetActive(true);
                screens[2].SetActive(false);
                break;
            case 2:
                screens[0].SetActive(false);
                screens[1].SetActive(false);
                screens[2].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        ScreenSet(2);
    }
}

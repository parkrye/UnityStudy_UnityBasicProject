using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MainScene;

public class MainScene : MonoBehaviour
{
    [SerializeField] GameObject Screen1, Screen2;
    [SerializeField] GameObject gamelist;
    [SerializeField] Button[] games;
    public enum GameScene { Main, Tank }
    [SerializeField] GameScene selectedGame;

    // Start is called before the first frame update
    void Awake()
    {
        Screen1.SetActive(true);
        Screen2.SetActive(false);
        selectedGame = GameScene.Tank;
        games = gamelist.GetComponentsInChildren<Button>();

        int game = 1;
        foreach(Button b in games)
        {
            int index = game;
            b.onClick.AddListener(() => OnGameSelectButton((GameScene)index));
            game++;
        }
    }

    public void OnStartButton()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnGameSelectButton(GameScene _selectGame)
    {
        selectedGame = _selectGame;
    }

    public void OnPlayButton()
    {
        switch(selectedGame)
        {
            case GameScene.Tank:
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                break;
        }
    }

    public void OncancelButton()
    {
        Screen1.SetActive(true);
        Screen2.SetActive(false);
        selectedGame = GameScene.Tank;
    }
}

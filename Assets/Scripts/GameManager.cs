using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager gameManager;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager GetGameManager()
    {
        if (gameManager == null)
        {
            return null;
        }
        return gameManager;
    }

    [SerializeField] int score = 0, life = 10;
    [SerializeField] bool ready = true;

    public int Score { get { return score; } set { score = value; UIManager.GetUIManager().UpdateUI(); } }
    public int Life { get { return life; } set { life = value; UIManager.GetUIManager().UpdateUI(); if (life == 0) UIManager.GetUIManager().GameOver(); } }
    public bool Ready { get { return ready; } set { ready = value; UIManager.GetUIManager().UpdateUI(); } }
}

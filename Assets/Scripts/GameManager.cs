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

    int score;

    public void AddScore(int add = 1)
    {
        score += add;
        Debug.Log(score);
    }

    public int GetScore()
    {
        return score;
    }
}

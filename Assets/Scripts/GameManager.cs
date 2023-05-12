using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    [SerializeField] GameObject tank;
    [SerializeField] CameraController cameraController;
    [SerializeField] int score = 0, life = 10;

    public enum ShotMode { Ready, Shot, Reload }
    [SerializeField] ShotMode shot = ShotMode.Ready;

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

    private void Start()
    {
        tank.GetComponent<CannonController>().AddObserver(UIManager.GetUIManager());
        tank.GetComponent<CannonController>().AddObserver(cameraController);
        tank.GetComponent<TankController>().AddObserver(UIManager.GetUIManager());
    }

    public int Score { get { return score; } set { score = value; } }
    public int Life { get { return life; } set { life = value > 100 ? 100 : value; } }
    public ShotMode Shot { get { return shot; } set { shot = value; } }
}

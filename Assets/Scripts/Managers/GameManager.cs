using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public static event System.Action<GameState> OnGameStateChanged;
    public static event System.Action OnScoreChanged;

    [SerializeField] GameObject Pipe;

    public int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Idle);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (state == GameState.Idle)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateGameState(GameState.Play);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) == true)
            Application.Quit();
    }

    void SpawnPipe()
    {
        Vector3 position = new Vector3(12f, Random.Range(2.08f, 6.55f), 0);
        GameObject newPipe = Instantiate(Pipe, position, Quaternion.identity);
    }

    public void UpdateScore()
    {
        score += 1;
        OnScoreChanged?.Invoke();
    }

    public void Retry()
    {
        EventSystem.current.SetSelectedGameObject(null, null);
        UpdateGameState(GameState.Idle);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Idle:
                HandleIdle();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleIdle()
    {
        score = -1;
        UpdateScore();
        Cursor.visible = false;
    }
    private void HandlePlay()
    {
        InvokeRepeating("SpawnPipe", 0f, 2.0f);
    }
    private void HandleLose()
    {
        CancelInvoke("SpawnPipe");
        Cursor.visible = true;
    }
}

public enum GameState
{
    Idle,
    Play,
    Lose
}

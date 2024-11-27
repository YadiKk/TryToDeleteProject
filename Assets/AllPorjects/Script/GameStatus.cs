using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    [Header("script")]
    public GameandLevelMaanager GameandLevelMaanagerScript;
    public CubeScr CubeScrScript;
    public MoveCube MoveCubeScript;

    [Header("bool")]
    public bool GameStart = false;
    public bool GameLoading = false;
    public float loadingValueTime;
    public float loadingValue;

    [Space(12)]
    [Header("UI")]
    [SerializeField] GameObject GameStopPanel;
    [SerializeField] GameObject loadingPanel;

    private void Start()
    {
        Time.timeScale = 1;
        GameandLevelMaanagerScript = FindAnyObjectByType<GameandLevelMaanager>();
        CubeScrScript = FindAnyObjectByType<CubeScr>();
        MoveCubeScript = FindAnyObjectByType<MoveCube>();
        GameandLevelMaanagerScript.enabled = GameStart;
        CubeScrScript.enabled = GameStart;
        MoveCubeScript.enabled = GameStart;
        GameLoading = true;

    }
    private void Update()
    {
        if (!GameStart && GameLoading)
        {
            loadingPanel.SetActive(true);
            Loading_Game();
        }

        else
        {
            loadingPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStart = !GameStart;
            GamePauseAction();
        }
    }

    void Loading_Game()
    {

        loadingValue += Time.deltaTime;
        Debug.Log(Mathf.FloorToInt(loadingValue));
        if (Mathf.FloorToInt(loadingValue) >= loadingValueTime)
        {
            GameStart = !GameStart;
            GamePauseAction();
            GameLoading = false;
            return;
        }
    }

    public void GameQuit_Btn()
    {
        Application.Quit();
    }
    public void GameMenu_Btn()
    {

        SceneManager.LoadScene(0);
    }
    public void GameStart_Btn()
    {
        Time.timeScale = 1;
        Scene CurrnetScne = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrnetScne.buildIndex + 1);
    }
    public void GameRestart_Btn()
    {
        Time.timeScale = 1;
        Scene CurrnetScne = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrnetScne.buildIndex);
    }
    public void GameContinueandStop_Btn()
    {
        GameStart = !GameStart;
        GamePauseAction();
    }

    public void GamePauseAction()
    {


        GameandLevelMaanagerScript.enabled = GameStart;
        CubeScrScript.enabled = GameStart;
        MoveCubeScript.enabled = GameStart;
        GameStopPanel.SetActive(!GameStart);
    }
}
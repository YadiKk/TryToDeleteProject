//using Unity.VisualScripting;
//using UnityEngine;
//using TMPro;

//public class GameandLevelMaanager : MonoBehaviour
//{
//    [Header("Scripts")]
//    public MoveCube MovecubeScript;
//    public CubeScr cubeScrScript;
//    GameStatus gameStatusScript;

//    [Space(12)]
//    [Header("Time Cost")]
//    [SerializeField] float delayTime;
//    float savedelaytime;

//    [Space(12)]
//    [Header("Text Mesh")]
//    [SerializeField] TextMeshProUGUI Time_text;
//    [SerializeField] TextMeshProUGUI StepCout_txt;
//    [SerializeField] TextMeshProUGUI ActiveCube_txt;

//    [Space(12)]
//    [Header("UI")]
//    [SerializeField] GameObject WinnerUI;
//    [SerializeField] GameObject LoseUI;


//    private void Start()
//    {
//        Time.timeScale = 1;
//        savedelaytime = delayTime;
//        if (MovecubeScript == null) MovecubeScript = GameObject.FindAnyObjectByType<MoveCube>();
//        if (cubeScrScript == null) cubeScrScript = GameObject.FindAnyObjectByType<CubeScr>();
//        if (gameStatusScript == null) gameStatusScript = GameObject.FindAnyObjectByType<GameStatus>();
//    }

//    private void Update()
//    {
//        ActiveCube_txt.text = cubeScrScript.activeCubeCount.ToString();
//        StepCout_txt.text = MovecubeScript.NowstepCount.ToString();
//        if (MovecubeScript.status == CubeStatus.CantMoveNotfinish)
//        {
//            delayTime -= Time.deltaTime;    
//            Time_text.text = Mathf.Clamp(Mathf.FloorToInt(delayTime),0, savedelaytime).ToString();
            
//            if (Mathf.FloorToInt(delayTime) < 0.2f)
//            {
//                if (cubeScrScript.activeCubeCount >= 1)
//                {
//                    MovecubeScript.status = CubeStatus.Lose;
//                    Time_text.text = "LOSE!";
                    
//                    WinnerUI.SetActive(false);
//                    LoseUI.SetActive(true);
//                    gameStatusScript.GameStart = false;
//                    gameStatusScript.GameandLevelMaanagerScript.enabled = gameStatusScript.GameStart;
//                    gameStatusScript.CubeScrScript.enabled = gameStatusScript.GameStart;
//                    gameStatusScript.MoveCubeScript.enabled = gameStatusScript.GameStart;
                    
//                    Time.timeScale = 0;
//                }
//                else
//                {
//                    Time.timeScale = 1;
//                    MovecubeScript.status = CubeStatus.Winner;
//                    WinnerUI.SetActive(true);
//                    LoseUI.SetActive(false);
//                    Time_text.text = "WINNER!";
//                    gameStatusScript.GameStart = false;
//                    gameStatusScript.GameandLevelMaanagerScript.enabled = gameStatusScript.GameStart;
//                    gameStatusScript.CubeScrScript.enabled = gameStatusScript.GameStart;
//                    gameStatusScript.MoveCubeScript.enabled = gameStatusScript.GameStart;


//                }
                
//            }
            

//        }
//        else if(MovecubeScript.status == CubeStatus.CanMove)
//        {
//            Time.timeScale = 1;
//            delayTime = savedelaytime;
            
//            Time_text.text = null;
//        }   
        
//    }
//}

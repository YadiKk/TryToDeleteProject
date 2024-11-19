using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameandLevelMaanager : MonoBehaviour
{
    [Header("Scripts")]
    public MoveCube MovecubeScript;
    public CubeScr cubeScrScript;

    [Space(12)]
    [Header("Time Cost")]
    [SerializeField] float delayTime;
    float savedelaytime;

    [Space(12)]
    [Header("Text Mesh")]
    [SerializeField] TextMeshProUGUI Time_text;
    [SerializeField] TextMeshProUGUI StepCout_txt;
    [SerializeField] TextMeshProUGUI ActiveCube_txt;

    private void Start()
    {
        savedelaytime = delayTime;
        if (MovecubeScript == null) MovecubeScript = GameObject.FindAnyObjectByType<MoveCube>();
        if (cubeScrScript == null) cubeScrScript = GameObject.FindAnyObjectByType<CubeScr>();
    }

    private void Update()
    {
        ActiveCube_txt.text = cubeScrScript.activeCubeCount.ToString();
        StepCout_txt.text = MovecubeScript.NowstepCount.ToString();
        if (MovecubeScript.status == CubeStatus.CantMoveNotfinish)
        {
            delayTime -= Time.deltaTime;    
            Time_text.text = Mathf.Clamp(Mathf.FloorToInt(delayTime),0, savedelaytime).ToString();
            
            if (Mathf.FloorToInt(delayTime) < 0.2f)
            {
                if (cubeScrScript.activeCubeCount >= 1)
                {
                    Time_text.text = "LOSE!";
                    MovecubeScript.status = CubeStatus.Lose;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                    Time_text.text = "WINNER!";
                    MovecubeScript.status = CubeStatus.Winner;
                    
                }
                
            }
            

        }
        else if(MovecubeScript.status == CubeStatus.CanMove)
        {
            Time.timeScale = 1;
            delayTime = savedelaytime;
            
            Time_text.text = null;
        }   
        
    }
}

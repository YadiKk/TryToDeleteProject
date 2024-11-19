using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
   public void StarGame_Btn()
    {
        Scene scen = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scen.buildIndex + 1);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}

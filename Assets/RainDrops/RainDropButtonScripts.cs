using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class RainDropButtonScripts : MonoBehaviour {



    public void PlayAgain()
    {
        SceneManager.LoadScene("RainDropScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}

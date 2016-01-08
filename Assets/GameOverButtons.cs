using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour {


    public void PlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}

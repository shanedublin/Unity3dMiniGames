using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MemoryGameOverButtons : MonoBehaviour {


    public void PlayAgain()
    {
        SceneManager.LoadScene("MemoryScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}

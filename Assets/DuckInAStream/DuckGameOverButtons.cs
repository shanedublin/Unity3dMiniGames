using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class DuckGameOverButtons : MonoBehaviour {
    public void PlayAgain()
    {
        SceneManager.LoadScene("DuckScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	
    public void StartRainDropLevel()
    {
        SceneManager.LoadScene("RainDropScene");
    }
}

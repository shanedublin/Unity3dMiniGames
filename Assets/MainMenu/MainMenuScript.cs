using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	
    public void StartRainDropLevel()
    {
        SceneManager.LoadScene("RainDropScene");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void StartMemoryLevel()
    {
        SceneManager.LoadScene("MemoryScene");
    }
    public void StartDuckLevel()
    {
        SceneManager.LoadScene("DuckScene");
    }
    public void StartPeaLevel()
    {
        SceneManager.LoadScene("PeaScene");
    }
    public void StartAlienLevel() {
        SceneManager.LoadScene("AlienTD");
    }

}

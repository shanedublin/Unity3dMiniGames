using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MemorySceneScript : MonoBehaviour {


    List<int> pattern;
    int nextPattern = 0;

    [SerializeField]    GameObject gameOverStuff;
    [SerializeField]    GameObject buttonPanel;
    [SerializeField]   Text highScoreText;

    [SerializeField]    Button[] buttons;

	// Use this for initialization
	void Start () {
        StartCoroutine(InitialStart());
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void PressedButton(int number)
    {
        //Debug.Log("pressed" + number);
        if(number == pattern[nextPattern])
        {
           
            nextPattern++;
            if(nextPattern  >= pattern.Count)
            {
                nextPattern = 0;
                addIntToPattern();
                StartCoroutine(ShowPattern());    
            }

        }
        else
        {            
            UpdateHighScore();
            buttonPanel.SetActive(false);
            gameOverStuff.SetActive(true);
        }
    }

    void addIntToPattern()
    {
        int rand = Random.Range(0, 9);
        pattern.Add(rand);
    }

    IEnumerator ShowPattern()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        yield return new WaitForSeconds(.5f);
        foreach (int i in pattern) {
            buttons[i].image.color = Color.blue;
            yield return new WaitForSeconds(.5f);
            buttons[i].image.color = Color.white;
            yield return new WaitForSeconds(.5f);
        }

        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
    IEnumerator InitialStart()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        pattern = new List<int>();
        addIntToPattern();
        yield return new WaitForSeconds(.5f);
        StartCoroutine(ShowPattern());
    }

    void UpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("MemoryHighScore");
        if (pattern.Count -1 > highScore)
        {
            highScore = pattern.Count -1;
            PlayerPrefs.SetInt("MemoryHighScore", pattern.Count -1);
        }

        highScoreText.text = "HighScore: " + highScore;
    }


}

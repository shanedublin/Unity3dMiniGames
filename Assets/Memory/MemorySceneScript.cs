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

    [SerializeField]    Text currentScore;

    [SerializeField]    Button[] buttons;

    [SerializeField]
    AudioClip[] buttonClicks;
    [SerializeField]
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        StartCoroutine(InitialStart());
	}
	
	// Update is called once per frame
	void Update () {

        CheckInput();
    }
    void CheckInput()
    {
        if (!buttons[0].interactable)
            return;
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            buttons[0].onClick.Invoke();

        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            buttons[1].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            buttons[2].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            buttons[3].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            buttons[4].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            buttons[5].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            buttons[6].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            buttons[7].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            buttons[8].onClick.Invoke();
        }
    }

    public void PressedButton(int number)
    {
        audioSource.PlayOneShot(buttonClicks[0]);
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
        currentScore.text = (pattern.Count -1).ToString();
    }

    IEnumerator ShowPattern()
    {
        yield return new WaitForSeconds(.5f);
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

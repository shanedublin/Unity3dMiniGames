using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RainDropper : MonoBehaviour
{

    [SerializeField]    AudioSource audioSource;
    [SerializeField]    AudioClip[] dropSounds;
    [SerializeField] float deltaX = 8;
    [SerializeField] float yStart = 8;

    [SerializeField] GameObject GameOverStuff;
    [SerializeField] GameObject[] lives;
    [SerializeField] int remainingLives;

    [SerializeField]    Text text;
    [SerializeField]    Text HighScoreText;
    public int score = 0;

    public float spawnInterval = .5f;
    float timer;

    [SerializeField]
    GameObject RainDrop;
    // Use this for initialization
    void Start()
    {
        remainingLives = lives.Length;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0) {
            timer = spawnInterval;
            GameObject drop = Instantiate(RainDrop);
            float randX = Random.Range(-deltaX, deltaX);
            drop.transform.position = new Vector3(randX, yStart);
        }

    }

    public void CaughtRainDrop()
    {
        score++;

        text.text = score.ToString("0");
        int dropSound = Random.Range(0, dropSounds.Length);
        audioSource.PlayOneShot(dropSounds[dropSound]);
    }

    public void MissedRainDrop()
    {
        if(remainingLives == 0)
        {
            // GameOver
            Time.timeScale = 0;
            GameOverStuff.SetActive(true);
            int highScore = PlayerPrefs.GetInt("RainDropHighScore");
            if(score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("RainDropHighScore", score);
            }

            HighScoreText.text = "HighScore: " + highScore;
            return;
        }

        remainingLives--;
        lives[remainingLives].SetActive(false);

    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DuckGameLogicScript : MonoBehaviour
{

    [SerializeField]
    Text highScoreText;
    [SerializeField]
    Text currentScoreText;

    [SerializeField]
    RiverObject swirl;
    [SerializeField]
    RiverObject log;
    [SerializeField]
    RiverObject rock;
    [SerializeField]
    float deltaX = 5f;
    [SerializeField]
    float spawnRate = 1f;
    float timer;
    [SerializeField]
    float riverRate = 5f;
    [SerializeField]
    int numLogs = 28;
    [SerializeField]
    int gapSize = 4;


    [SerializeField]
    GameObject gameOverStuff;

    [SerializeField]    AudioSource audioSource;

    [SerializeField]    AudioClip DuckdeathSound;
    float swirlSpawnRate = 3f;
    float swirlTimer;

    float rockSpawnRate = 3;
    float rockTimer;

    int score = 0;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        swirlTimer = swirlSpawnRate;
        rockTimer = rockSpawnRate;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            timer = spawnRate;
            spawnWave();

        }
        SwirlLogic();
        RockLogic();
    }

    void RockLogic()
    {
        rockTimer -= Time.fixedDeltaTime;
        if (rockTimer <= 0)
        {
            rockTimer = Random.Range(2, rockSpawnRate);
            RiverObject rockClone = Instantiate(rock);
            rockClone.transform.position = new Vector3(Random.Range(-8.5f, 8.5f), transform.position.y);
            rockClone.body.velocity = new Vector2(0, -riverRate *.7f);
        }
    }

    void SwirlLogic()
    {
        swirlTimer -= Time.fixedDeltaTime;
        if (swirlTimer <= 0)
        {
            swirlTimer = Random.Range(1, swirlSpawnRate);
            RiverObject swirlClone = Instantiate(swirl);
            swirlClone.transform.position = new Vector3(Random.Range(-8.5f, 8.5f), transform.position.y);
            swirlClone.body.velocity = new Vector2(0, -riverRate * 1.1f);
        }
    }
    void spawnWave()
    {
        incrScore(1);
        RiverObject[] obs = new RiverObject[numLogs];
        for (int i = 0; i < numLogs; i++)
        {
            RiverObject logClone = Instantiate(log);
            logClone.transform.position = new Vector3(transform.position.x + i * .7f, transform.position.y);
            logClone.body.velocity = new Vector2(0, -riverRate);
            obs[i] = logClone;
        }

        int rand = Random.Range(0, numLogs - gapSize);

        for (int i = rand; i < rand + gapSize; i++)
        {
            Destroy(obs[i].gameObject);
        }
    }

    public void incrScore(int i )
    {
        score += i;
        currentScoreText.text = score.ToString();
    }
    public void DeathOfDuck()
    {
        audioSource.PlayOneShot(DuckdeathSound,1);
        gameOverStuff.SetActive(true);
        int highScore = PlayerPrefs.GetInt("DuckHighScore");
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("DuckHighScore", score);
        }

        highScoreText.text = "HighScore: " + highScore;
    }

   
}

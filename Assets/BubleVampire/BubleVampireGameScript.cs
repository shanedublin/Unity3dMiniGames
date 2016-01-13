using UnityEngine;
using System.Collections;


public class BubleVampireGameScript : MainGameLogic
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip popSounds;


    [SerializeField]
    BubleFoodScript bubleFood;
    [SerializeField]
    StakeScript stake;


    public float spawnRate;
    float timer;


    public float stakeSpawnRate;
    [SerializeField]
    float stakeTimer;
    [SerializeField]
    BubleVampPlayerScript bvps;




    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    public override void UpdateHighScore()
    {

        float highScore = PlayerPrefs.GetFloat("BubleVampireHighScore");
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("BubleVampireHighScore", score);
        }

        highScoreText.text = "HighScore: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        stakeTimer -= Time.fixedDeltaTime;
        if(timer <= 0)
        {
            timer = spawnRate;
            BubleFoodScript bsf = Instantiate(bubleFood);
            bsf.transform.position = new Vector3(Random.Range(-9f, 9f),Random.Range(-4,0));

        }

        if(stakeTimer <= 0)
        {
            stakeTimer = 8/(score+1);
            StakeScript ss = Instantiate(stake);
            ss.transform.position = new Vector3(Random.Range(-8f, 8f), 5);
        }
    }

    public void GotBuble(float size)
    {
        score += size;
        currentScoreText.text = score.ToString();
        audioSource.pitch = Random.Range(.7f, 1.2f);
        audioSource.PlayOneShot(popSounds, .1f);
    }
    public override void GameOver()
    {
        base.GameOver();
        audioSource.pitch = Random.Range(.7f, 1.2f);
        audioSource.PlayOneShot(popSounds, .1f);
    }
}

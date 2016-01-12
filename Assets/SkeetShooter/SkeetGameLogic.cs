using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SkeetGameLogic : MainGameLogic {

    [SerializeField]
    Text livesText;
    [SerializeField]
    Text ammoText;

    [SerializeField]
    GameObject cloud;
    [SerializeField]
    TargetScript target;

    [SerializeField]
    LayerMask mask;


    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] skeetSpawnSound;

    [SerializeField]
    AudioClip[] skeetDestroySound;

    [SerializeField]
    AudioClip bulletSound;

    [SerializeField]
    AudioClip emptySound;

    public int wave = 1;
    bool waveOver = false;
    public int score = 0;
    
    public int lives = 3;
    public int bullets = 0; 

    

	// Use this for initialization
	public override void Start () {
        base.Start();
        //StartCoroutine(DoWave());
        // reload();
        StartCoroutine(StartMessage());
    }

    IEnumerator StartMessage()
    {
        waveText.text = "Good Luck";
        yield return new WaitForSeconds(1f);
        waveText.CrossFadeAlpha(0, 3f, true);
        yield return new WaitForSeconds(3.5f);
        waveOver = true;
    }


    public override void UpdateHighScore()
    {
        
        int highScore = PlayerPrefs.GetInt("SkeetScore");
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("SkeetScore", score);
        }

        highScoreText.text = "HighScore: " + highScore;
    }

    // Update is called once per frame
    void Update () {

        if (waveOver  && ! gameOver)
        {
            waveOver = false;
            // reload
            reload();
            StartCoroutine(DoWave());
        }
        if (Input.GetMouseButtonDown(0) )
        {
            if(bullets > 0)
            {

            UseBullet();
            //RaycastHit2D hit = new Ray
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 999,mask);

            if (hit.collider != null)
            {
                Debug.Log("Hit" + hit.collider.gameObject);

                TargetScript targetScript = hit.collider.GetComponent<TargetScript>();
                if (targetScript)
                {
                    targetScript.Damage();
                    incrScore();
                    audioSource.PlayOneShot(skeetDestroySound[Random.Range(0, skeetDestroySound.Length)]);
                }   

            }
            }
            else
            {
                audioSource.PlayOneShot(emptySound);
            }

            // check if bullets
            // raycast to see if hit
            // destroy targt incr score
            // desntroyed enough targts
            // get life
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            // reload?
        }
	}

    void incrScore()
    {
        if (gameOver)
            return;
        score++;
        currentScoreText.text = score.ToString();
        if(score % 25 == 0)
        {
            BonusLife();
        }
    }

    void reload()
    {
        bullets += (int) (wave * 1.1f + 4);
        ammoText.text = "x" + bullets.ToString();
    }

    IEnumerator TweenWaveText()
    {
        waveText.text = "Wave " + wave;
        waveText.CrossFadeAlpha(1, 1f, true);
        yield return new WaitForSeconds(1f);
        waveText.CrossFadeAlpha(0, 2f, true);
    }
    IEnumerator DoWave()
    {
        StartCoroutine(TweenWaveText());
        yield return new WaitForSeconds(3f);
        // set bullet count
        // spawn target
        float time = timerMath();
        for (int i = 0; i < wave + 2; i++)
        {
            float randX = Random.Range(-9, 9);
            float yVel = Random.Range(9, 14);
            float xVel;
            if(randX > 0)
            {
                xVel = Random.Range(-2, 2 - (randX / 9 )* 2);
            }
            else
            {
                xVel = Random.Range(-2 - (randX/9) *2, 2 );
            }
            TargetScript targetClone = Instantiate(target);
            targetClone.transform.position = new Vector3(randX, -7);
            targetClone.SetVeloctity(new Vector2(xVel,yVel));
            audioSource.PlayOneShot(skeetSpawnSound[Random.Range(0, skeetSpawnSound.Length)]);
            yield return new WaitForSeconds(time);
        }
        yield return new WaitForSeconds(1.5f);
        wave++;
        waveOver = true;
    }

    float timerMath()
    {
        return Mathf.Clamp( 1 - (wave * .025f),.5f,9);
    }
    public void BonusLife()
    {
        lives++;
        livesText.text = "x" + lives;
    }

    public void MissedTarget()
    {
        if (gameOver)
            return;
        lives--;
        livesText.text = "x" + lives;
        if(lives <= 0)
        {
            
            GameOver();
            gameOver = true;
        }
    }

    void UseBullet()
    {
        audioSource.PlayOneShot(bulletSound);
        bullets--;
        ammoText.text = "x" + bullets.ToString();
    }

}

using UnityEngine;
using System.Collections;


public class StackingGameLogic : MainGameLogic
{

    [SerializeField]
    BlockScript Block;
    [SerializeField]
    Camera maincamera;

    Vector3 newCamSpot;

    [SerializeField]
    float camTransitionSpeed;

    bool firstBlock = true;

    bool readyForNextBlock = true;
    [SerializeField]
    bool phase1 = false;
    [SerializeField]
    bool phase2 = false;
    [SerializeField]
    bool phase3 = false;

    BlockScript block;
    float timer;

    [SerializeField]
    float changeSpeed = 2;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        newCamSpot = maincamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            if (phase1)
            {
                phase1 = false;
                phase2 = true;
                changeSpeed = 2 + score * .1f;
                timer = 90f *  Mathf.Deg2Rad;
                
            }
            else if (phase2)
            {
                phase2 = false;
                phase3 = true;
            }
        }
    }

    public void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (readyForNextBlock && !gameOver)
        {
            block  = Instantiate(Block);
            block.SetKinematic(true);
            Vector2 pos = newCamSpot;
            pos.y += 3;
            block.transform.position = pos;
            phase1 = true;
            readyForNextBlock = false;

            block.canTouchGround = firstBlock;
            firstBlock = false;
            block.sgl = this;
           
        }

        if (phase1)
        {
            Vector2 pos = block.transform.position;
            pos.x = Mathf.Cos(timer  * changeSpeed) * 8;
            block.transform.position = pos;            
        }else if (phase2)
        {
            Vector2 scale = block.transform.localScale;
            scale.x = Mathf.Cos(timer * changeSpeed)/2 + 1.5f;
            block.transform.localScale = scale;

        }
        else if(phase3)
        {
            block.SetKinematic(false);
            phase3 = false;
        }

    }

    public void LateUpdate()
    {
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, newCamSpot, camTransitionSpeed);
    }

    public override void UpdateHighScore()
    {

        float highScore = PlayerPrefs.GetFloat("StackingHighScore");
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("StackingHighScore", score);
        }

        highScoreText.text = "HighScore: " + highScore;
    }

    public void lost()
    {
        Debug.Log("Game over");
        GameOver();
    }
    public void NextBlock()
    {
        if (gameOver)
            return;
        Debug.Log("ready for next block");
        readyForNextBlock = true;
        score++;
        currentScoreText.text = score.ToString();
        newCamSpot.y= block.transform.position.y + 2;
        changeSpeed = .5f + score * .05f;
        timer = Random.Range(0f,360f) * Mathf.Deg2Rad;
    }



}

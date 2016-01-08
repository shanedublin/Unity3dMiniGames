using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeaGameLogic : MonoBehaviour
{

    [SerializeField]
    GameObject peaPod;

    [SerializeField]
    GameObject plate;

    [SerializeField]
    GameObject peaBody;

    [SerializeField]
    float camSpeed = 1.5f;


    [SerializeField]
    GameObject gameOverStuff;
    [SerializeField]
     Text scoreText;
    [SerializeField]
    Text highScoreText;

    [SerializeField]
    float peaBodySpawnRate = 5;

    [SerializeField]
    float plateSpawnRate = 5;

    int currentScore = 0;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnPeaBody());
        StartCoroutine(SpawnPeaPod());
        StartCoroutine(SpawnPlate());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void LateUpdate()
    {
        Vector3 pos = Camera.main.gameObject.transform.position;
        pos.x += Time.deltaTime * camSpeed;
        Camera.main.gameObject.transform.position = pos;
    }

    public void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x += Time.fixedDeltaTime * camSpeed;
        transform.position = pos;
    }
    IEnumerator SpawnPeaBody()
    {
        yield return new WaitForSeconds(peaBodySpawnRate);
        GameObject peaClone= Instantiate(peaBody);
        Vector3 pos = transform.position;
        pos.y = Random.Range(-4.5f, 4.5f);
        peaClone.transform.position = pos;
        StartCoroutine(SpawnPeaBody());
    }


    IEnumerator SpawnPeaPod()
    {
        yield return new WaitForSeconds(peaBodySpawnRate*3);
        GameObject peaClone = Instantiate(peaPod);
        Vector3 pos = transform.position;
        pos.y = Random.Range(-4.5f, 4.5f);
        peaClone.transform.position = pos;
        StartCoroutine(SpawnPeaPod());

    }

    IEnumerator SpawnPlate()
    {
        yield return new WaitForSeconds(peaBodySpawnRate);
        GameObject plateClone = Instantiate(plate);
        Vector3 pos = transform.position;
        pos.y = Random.Range(-5f, 5f);
        plateClone.transform.position = pos;
        StartCoroutine(SpawnPlate());

    }

    public void GameOver()
    {
        gameOverStuff.SetActive(true);
        int highScore = PlayerPrefs.GetInt("PeaHighScore");
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("PeaHighScore", currentScore);
        }

        highScoreText.text = "HighScore: " + highScore;
    }

    public void IncrScore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }
}

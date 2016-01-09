using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AlienGameLogic : MonoBehaviour
{

    public int wave = 1;
    public int numEnemies = 4;
    [SerializeField]
    AudioClip[] animalDeathSounds;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    Text highScoreText;
    [SerializeField]
    Text pointsText;
    [SerializeField]
    GameObject turret;

    [SerializeField]
    GameObject alien;
    public float alienSpawnRate = .5f;
    float alienSpawnTimer = 0;
    [SerializeField]
    LayerMask pokeLayers;
    public float dist = 5;
    [SerializeField]
    GameObject GameOverStuff;

    static AlienGameLogic instance;

    public float points;
    public float score;
    public bool placeTurret;

    public float alienDistance;

    public float clickDamage;
    public int numTurrets = 0;
    // Use this for initialization
    void Start()
    {
        instance = this;
        Time.timeScale = 1;
        StartCoroutine(SpawnAlien());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit2D hit = new Ray
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,999,pokeLayers);

            if (hit.collider != null)
            {
                Debug.Log("Hit" + hit.collider.gameObject);

                AlienScript alienScript = hit.collider.GetComponent<AlienScript>();
                if (alienScript)
                {
                    alienScript.Damage(clickDamage);


                }
                TurretScript ts = hit.collider.GetComponent<TurretScript>();
                if (ts)
                {

                }



            }
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (points >= (numTurrets + 1 * 10))
        //    {
        //        points -= (numTurrets + 1 * 10);
        //        GameObject turr = Instantiate(turret);
        //        Vector3 MousePos = Input.mousePosition;
        //        Vector3 pos = Camera.main.ScreenToWorldPoint(MousePos);
        //        Debug.Log(pos);
        //        pos.z = 0;
        //        turret.transform.position = pos;
        //        TurretScript ts = turr.GetComponent<TurretScript>();
        //        numTurrets++;
        //        AlienDeath(0);
        //    }
        //}
    }


    public static void AlienDeath(float points)
    {
        instance.points += points;
        instance.pointsText.text = "$ " + instance.points.ToString();
        instance.audioSource.PlayOneShot(instance.animalDeathSounds[Random.Range(0, instance.animalDeathSounds.Length)]);
    }

    public void FixedUpdate()
    {
        
    }

    IEnumerator SpawnAlien()
    {
        GameObject alienClone = Instantiate(alien);
        float deg = Random.Range(0f, 360f);

        Vector3 pos = transform.position;
        pos.y = dist * Mathf.Sin(deg);
        pos.x = dist * Mathf.Cos(deg);
        alienClone.transform.position = pos;
        AlienScript alienScript = alienClone.GetComponent<AlienScript>();
        alienScript.maxHealth = 1 + (wave-1) * .25f;



        yield return new WaitForSeconds(alienSpawnRate - ((wave - 1) * .1f));
        if(numEnemies > 1)
        {
            numEnemies--;
            StartCoroutine(SpawnAlien());
        }
        else
        {
            wave++;
            numEnemies = 5 + wave;
            yield return new WaitForSeconds(5 - ((wave-1) * .1f));
            StartCoroutine(SpawnAlien());
        }
       

    }
    public void GameOver()
    {
        GameOverStuff.SetActive(true);
        Time.timeScale = 0;

        float highScore = PlayerPrefs.GetFloat("DuckHighScore");
        if (points > highScore)
        {
            highScore = points;
            PlayerPrefs.SetFloat("DuckHighScore", points);
        }

        highScoreText.text = "HighScore: " + highScore;

    }
}

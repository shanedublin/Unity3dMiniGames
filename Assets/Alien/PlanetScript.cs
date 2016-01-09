using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]    Sprite[] explosionSprites;

    [SerializeField]
    AlienGameLogic agl;
    public float currentHealth = 10;
    public float maxHealth = 10;

    public bool alive = true;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        AlienScript alien = other.GetComponent<AlienScript>();

        if (alien)
        {
            currentHealth -= alien.currentHealth;
                StartCoroutine(Damage());
            if(currentHealth <=0 )
            {
                KillPlanet();
            }
            else
            {
                Destroy(other.gameObject);
            }
        }


    }

    public void KillPlanet()
    {
        if (alive)
        {
            StartCoroutine(DeathAnim());
        }
    }

    public IEnumerator Damage()
    {
        sr.color = new Color(1, .5f, .5f);
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1, 1, 1);
    }

    public IEnumerator DeathAnim()
    {

        alive = false;
        GetComponent<Collider2D>().enabled = false;

        for (int i = 0; i < explosionSprites.Length; i++)
        {
            if (sr == null)
            {
                Debug.Log("hate hate hate");
            }
            else
            {
                sr.sprite = explosionSprites[i];
                yield return new WaitForSeconds(.1f);
            }
        }

        Destroy(gameObject);
        agl.GameOver();

    }

}

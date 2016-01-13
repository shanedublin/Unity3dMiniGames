using UnityEngine;
using System.Collections;

public class BubleVampPlayerScript : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] popSounds;



    public CircleCollider2D col;

    Vector3 mousePos;
    [SerializeField]
    Rigidbody2D body;
    bool mouseDown = false;
    [SerializeField]
    float turnRate = 360;

    [SerializeField]
    float peaSpeed = 100f;

    [SerializeField]
    float maxDuckSpeed = 20f;
    // Use this for initialization

    public bool alive = true;


    [SerializeField]
    BubleVampireGameScript bvgs;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void FixedUpdate()
    {
        if (alive)
        {
            Vector2 direction = mousePos - transform.position;
            float mag = direction.magnitude;
            mag = Mathf.Clamp(mag, 0, 1);
            direction = Vector3.Normalize(direction);
            body.AddForce(direction * Time.fixedDeltaTime * peaSpeed * mag);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BubleFoodScript bfs = collision.GetComponent<BubleFoodScript>();
        if (bfs != null)
        {
            //audioSource.PlayOneShot(popSounds[Random.Range(0, popSounds.Length)]);
            Destroy(bfs.gameObject);
            bvgs.GotBuble(1);
            float newSize = transform.localScale.x + bfs.col.radius / 3;
            transform.localScale = new Vector3(newSize, newSize);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        StakeScript ss = collision.gameObject.GetComponent<StakeScript>();
        if (ss != null)
        {
            //audioSource.PlayOneShot(popSounds[Random.Range(0, popSounds.Length)]);
            Destroy(gameObject);
            bvgs.GameOver();
            return;
        }

        CrossScript cs = collision.gameObject.GetComponent<CrossScript>();
        if (cs != null)
        {
            //audioSource.PlayOneShot(popSounds[Random.Range(0, popSounds.Length)]);
            Destroy(gameObject);
            bvgs.GameOver();

        }

    }
}

using UnityEngine;
using System.Collections;

public class AlienScript : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Rigidbody2D body;
    public float maxHealth = 1;
    public float currentHealth = 1;
    public float speed;


    float timer;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        transform.localScale = new Vector3(1 + (currentHealth *.1f), 1 + (currentHealth * .1f));
        body.rotation = AngleBetween(Vector3.zero, transform);
     }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = .25f;
            sr.flipX = !sr.flipX;
        }
    }

    public void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x - Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad) * speed * Time.fixedDeltaTime,
            transform.position.y + Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad) * speed * Time.fixedDeltaTime);
    }

    public void Damage(float i)
    {
        currentHealth -= i;
        transform.localScale = new Vector3(1 + (currentHealth * .1f), 1 + (currentHealth * .1f));
        StartCoroutine(Damage());
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            AlienGameLogic.AlienDeath(maxHealth);
        }
    }

    public IEnumerator Damage()
    {
        sr.color = new Color(1, .5f, .5f);
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1, 1, 1);
    }

    public static float AngleBetween(Vector3 look, Transform pos)
    {
        Vector3 enenmyToPlayer = (look) - pos.position;
        float angle = Vector2.Angle(enenmyToPlayer, pos.up);
        Vector3 cross = Vector3.Cross(enenmyToPlayer, pos.up);
        if (cross.z > 0)
            angle = -angle;
        return angle;
    }
}

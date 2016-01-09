using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TurretScript : MonoBehaviour
{

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    CircleCollider2D col;

    [SerializeField]
    GameObject smokeTrail;

    [SerializeField]
    GameObject rangeIndicator;
    public List<AlienScript> targets = new List<AlienScript>();
    public int level;
    public LayerMask targetLayers;
    public float range = 1;
    public float fireRate = 1;
    public float fireTimer;
    public float damage = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      ///  createSmokeTrail();
    }
    public void Upgrade()
    {
        level++;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("enemy entered");
        AlienScript alien = other.GetComponent<AlienScript>();

        if (alien != null)
        {
            targets.Add(alien);
        }
    }

    public void FixedUpdate()
    {
        fireTimer -= Time.fixedDeltaTime;

        if(fireTimer <= 0)
        {
            rangeIndicator.SetActive(true);
            
            foreach(AlienScript alien in targets)
            {
                if(alien == null)
                {
                    targets.Remove(alien);
                    break;
                }
               // Debug.Log(Vector3.Distance(alien.transform.position, transform.position));
                if(Vector3.Distance( alien.transform.position,transform.position) <= range + alien.gameObject.GetComponent<CircleCollider2D>().radius*2)
                {
                    Fire(alien);
                    break;
                }
                else
                {
                    targets.Remove(alien);
                    break;
                }
            }
        }
    }

     void Fire(AlienScript alien)
    {
        Debug.Log("Fire");
        Vector3 rot = transform.localEulerAngles;
        rot.z = AlienScript.AngleBetween(alien.transform.position, transform) + body.rotation;
        transform.localEulerAngles = rot;

        createSmokeTrail();
        fireTimer = 1/fireRate;
        alien.Damage(damage);
        rangeIndicator.SetActive(false);
        audioSource.Play();

    }




    void createSmokeTrail()
    {

        Vector3 smokePos = smokeTrail.transform.localPosition;
        Vector3 scale = smokeTrail.transform.localScale;
       // Debug.DrawRay(transform.position, transform.up *9, Color.red,.5f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up,range *5,targetLayers);
        if (hit.collider != null)
        {

            Debug.Log("hit something...");
            
            smokePos.y = hit.distance / 2;
            scale.y = hit.distance * 32;
            StartCoroutine(HideSmoke());

        }
        else
        {
            //smokePos.y =  range /2;
           /// scale.y = range * 32;
        }
        
        smokeTrail.transform.localPosition = smokePos;
        smokeTrail.transform.localScale = scale;
    }

    IEnumerator HideSmoke()
    {
        SpriteRenderer sr = smokeTrail.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 1);

        for (int i = 60; i >= 0; i -= 5)
        {
            //  Debug.Log("blah");
            sr.color = new Color(1, 1, 1, i / 60f);
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(.01f);

        }

    }
}

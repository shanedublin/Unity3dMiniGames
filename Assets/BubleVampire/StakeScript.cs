using UnityEngine;
using System.Collections;

public class StakeScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] thumpSounds;

    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    SpriteRenderer sr;

    bool grounded = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        if(body.velocity.magnitude <= 0.1f)
        {
            if (!grounded)
            {
            audioSource.PlayOneShot(thumpSounds[Random.Range(0, thumpSounds.Length)]);
                grounded = true;
            }
            sr.color = new Color(1, 1, 1, sr.color.a - 1 * Time.fixedDeltaTime);
            if(sr.color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class PeaPodScript : MonoBehaviour
{

    [SerializeField]

    Rigidbody2D body;

    [SerializeField]
    float spinSpeed = 10;

    public void Start()
    {
        body.angularVelocity = Random.Range(-spinSpeed, spinSpeed);
    }

    bool used = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!used)
        {
            PeaHeadController phc = other.GetComponent<PeaHeadController>();
            if (phc != null)
            {
                phc.HitPeaPod(gameObject);
                used = true;
            }

        }




    }
}

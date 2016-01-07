using UnityEngine;
using System.Collections;

public class WaterSwirlScript : RiverObject
{

    // Use this for initialization
    void Start()
    {
        //float speed = Random.Range(3f, 15f);
        //body.velocity = new Vector2(0, -speed);
        float spin = Random.Range(10, 85f);
        body.angularVelocity = spin;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        DuckScript ds = other.gameObject.GetComponent<DuckScript>();
        if (ds != null)
        {
            ds.GetSwirl();
            Destroy(gameObject);
            return;
        }
    }
}

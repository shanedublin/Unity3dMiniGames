using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        DuckScript ds = other.gameObject.GetComponent<DuckScript>();
        if (ds != null)
        {

            ds.KillDuck();
            return;
        }


        Destroy(other.gameObject);
    }

    
}

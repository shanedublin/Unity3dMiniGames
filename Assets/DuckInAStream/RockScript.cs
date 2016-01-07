using UnityEngine;
using System.Collections;

public class RockScript : RiverObject
{

  
    public void OnCollisionEnter2D(Collision2D other)
    {
        DuckScript ds = other.gameObject.GetComponent<DuckScript>();
        if(ds != null){

            ds.KillDuck();
            return;
        }
        
        Destroy(other.gameObject);
    }

   
}

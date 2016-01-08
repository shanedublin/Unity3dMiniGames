using UnityEngine;
using System.Collections;

public class PeaBody : MonoBehaviour
{

    public GameObject leader;
    [SerializeField]
    Rigidbody2D body;
    public float peaSpeed = 1000f;

    [SerializeField]
    float maxPeaSpeed = 20f;

    public PeaHeadController phc;

    [SerializeField]
    Collider2D col;
    public void FixedUpdate()
    {
        if(leader != null)
        {
            Vector2 direction = leader.transform.position - transform.position;
            float mag =   direction.sqrMagnitude;
            direction.Normalize();
           // Debug.Log(mag);
            if(mag > .3f)
                body.AddForce(direction * Time.fixedDeltaTime * peaSpeed * mag);


            if (body.velocity.magnitude > maxPeaSpeed)
            {
                body.velocity = body.velocity.normalized * maxPeaSpeed;
            }
        }
    }

    public void SetLeader(GameObject goleoader)
    {
        leader = goleoader;
        StartCoroutine(DelayedTrigger());
    }

    public void RemoveLeader()
    {
        leader = null;
        col.enabled = false;
        body.velocity = Vector2.zero;
       // Debug.Log("Removing Leader");
    }


    IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(.25f);
        col.isTrigger = false;
    }

    public void HitPlate(GameObject plate)
    {
        if(phc != null)
            phc.IHitAPlate(plate);
    }
}

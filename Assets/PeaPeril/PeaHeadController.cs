using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PeaHeadController : MonoBehaviour
{

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

    List<PeaBody> peaString = new List<PeaBody>();

    [SerializeField]
    PeaGameLogic pgl;


    GameObject leader;
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
        if (alive) {
        Vector2 direction = mousePos - transform.position;
        float mag = direction.magnitude;
        mag = Mathf.Clamp(mag, 0, 1);
        direction = Vector3.Normalize(direction);
        body.AddForce(direction * Time.fixedDeltaTime * peaSpeed * mag);
        }
        else
        {
            if(leader == null)
            {
                return;
            }
            Vector2 direction = leader.transform.position - transform.position;
            float mag = direction.sqrMagnitude;
            direction.Normalize();
            // Debug.Log(mag);
            if (mag > .3f)
                body.AddForce(direction * Time.fixedDeltaTime * peaSpeed * mag);


        }



    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PeaBody pb = other.GetComponent<PeaBody>();
        if(pb != null && pb.leader == null)
        {
            if(peaString.Count == 0)
            {
                pb.SetLeader(gameObject);
            }
            else
            {
                pb.SetLeader(peaString[peaString.Count -1].gameObject);
            }
                peaString.Add(pb);
            pb.phc = this;
        }
    }

    public void IHitAPlate(GameObject plate)
    {
        // kill everthing 2 up and everything back.
        pgl.GameOver();
        PeasToPlate(plate);
        alive = false;
    }
    public void HitPlate(GameObject plate)
    {
        alive = false;
        pgl.GameOver();
        PeasToPlate(plate);
    }

    public void PeasToPlate(GameObject plate)
    {

        leader = plate;
        foreach(PeaBody pb in peaString)
        {
            pb.SetLeader(plate);
        }
    }
    public bool HitPeaPod(GameObject pod)
    {

        if (!alive)
            return true;
        bool scored = false;
        //Debug.Log("Hit a pea pod");
        int count = peaString.Count;
        for (int i = count -1, j = 0; i >= 0 && j < 3; i-- , j ++)
        {
            PeaBody pbody = peaString[i];
            peaString.RemoveAt(i);
            pbody.RemoveLeader();
            pbody.transform.SetParent(pod.transform);
            pbody.transform.localPosition = new Vector3(0, (j - 1)* .35f ,0);
            //pbody.transform.position = pod.gameObject.transform.position;
            scored = true;
        }
        pgl.IncrScore(count * count);
        return scored;
        
    }
}

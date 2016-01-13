using UnityEngine;
using System.Collections;

public class ThatsNoMoonScript : MonoBehaviour
{

    public float time;
    public float speed;
    // Use this for initialization
    void Start()
    {

        Vector3.Lerp(transform.position,new Vector3(10,transform.position.y,0), time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime , transform.position.y, 0);
        
        
    }
}

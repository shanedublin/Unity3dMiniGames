using UnityEngine;
using System.Collections;

public class BucketScript : MonoBehaviour
{
    [SerializeField]
    RainDropper rainDropper;

    Vector3 mousePos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        }
    }

    public void FixedUpdate()
    {
        Vector3 t = mousePos;
        t.z = 0;
        transform.position = t;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Cought");
        RainDropScript rd = other.GetComponent<RainDropScript>();
        if(rd != null)
        {
            Destroy(other.gameObject);
            rainDropper.CaughtRainDrop();
        }
    }
}

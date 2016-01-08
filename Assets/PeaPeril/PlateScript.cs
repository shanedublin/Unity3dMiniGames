using UnityEngine;
using System.Collections;

public class PlateScript : MonoBehaviour
{

    float xRand;
    float yRand;

    public void Start()
    {
        xRand = Random.Range(-2, 2);
        yRand = Random.Range(1, 2);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pea"))
        {
            PeaBody pb = other.GetComponent<PeaBody>();
            if (pb != null)
            {
                pb.HitPlate(this.gameObject);
            }
            else
            {
                PeaHeadController peaHead = other.GetComponent<PeaHeadController>();
                if (peaHead != null)
                {
                    peaHead.HitPlate(gameObject);
                }
            }
        }
    }

    public void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.time * xRand) * 2 * Time.fixedDeltaTime,
                            transform.position.y + Mathf.Cos(Time.time * yRand) * 2 * Time.fixedDeltaTime);
    }
}

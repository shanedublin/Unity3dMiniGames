using UnityEngine;
using System.Collections;

public class TargetDestroyerScript : MonoBehaviour
{
    [SerializeField]
    SkeetGameLogic sgl;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        TargetScript ts = collision.GetComponent<TargetScript>();
        if(ts != null)
        {
            sgl.MissedTarget();
            Destroy(collision.gameObject);
        }
    }
}

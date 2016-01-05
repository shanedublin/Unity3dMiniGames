using UnityEngine;
using System.Collections;

public class DeleterScript : MonoBehaviour
{
    [SerializeField]
    RainDropper rainDropper;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        rainDropper.MissedRainDrop();
    }
}

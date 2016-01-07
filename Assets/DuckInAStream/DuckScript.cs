using UnityEngine;
using System.Collections;

public class DuckScript : MonoBehaviour
{

    [SerializeField]
    DuckGameLogicScript dgls;


    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] swirlSounds;

    

    public void KillDuck()
    {
        Destroy(gameObject);
        dgls.DeathOfDuck();
    }
    public void GetSwirl()
    {
        dgls.incrScore(5);
        audioSource.PlayOneShot(swirlSounds[Random.Range(0, swirlSounds.Length)]);
    }


}

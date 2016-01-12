using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    Sprite[] explosionSprites;


    [SerializeField]
    CircleCollider2D col;

    [SerializeField]
    Rigidbody2D body;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Damage()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        col.enabled = false;
        foreach(Sprite sprite in explosionSprites)
        {
            sr.sprite = sprite;
            yield return new WaitForSeconds(.02f);
        }
        Destroy(this.gameObject);
    }

    public void SetVeloctity(Vector2 vel)
    {
        body.velocity = vel;
    }

}

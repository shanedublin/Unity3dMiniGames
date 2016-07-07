using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Sprite[] colors;
    public bool canTouchGround = false;
    public StackingGameLogic sgl;


    bool stoppedMoving = false;

    float timer = .5f;
    // Use this for initialization
    void Start()
    {
        sr.sprite = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetKinematic(bool buul)
    {
        body.isKinematic = buul;
    }
    public void FixedUpdate()
    {
        if (body.isKinematic)
            return;
        
        if (stoppedMoving)
        {
            if(body.velocity.magnitude > maxSpeed)
            {
               // sgl.lost();
            }

        }
        else
        {
            timer -= Time.fixedDeltaTime;
            if(timer <= 0)
                if(body.velocity.magnitude < maxSpeed)
                {
                    stoppedMoving = true;
                    sgl.NextBlock();
                }
        }
    }
}

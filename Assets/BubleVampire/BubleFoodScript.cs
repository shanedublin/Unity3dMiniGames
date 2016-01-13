using UnityEngine;
using System.Collections;

public class BubleFoodScript : MonoBehaviour
{

    [SerializeField]
    public CircleCollider2D col;
    [SerializeField]
    Rigidbody2D body;
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    Vector2 velocity;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        velocity = body.velocity;
        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }
}

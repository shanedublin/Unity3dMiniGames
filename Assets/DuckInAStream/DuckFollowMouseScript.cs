using UnityEngine;
using System.Collections;

public class DuckFollowMouseScript : MonoBehaviour
{

    Vector3 mousePos;
    [SerializeField]    Rigidbody2D body;
    bool mouseDown = false;
    [SerializeField]    float turnRate = 360;

    [SerializeField]
    float duckSpeed = 100f;

    [SerializeField]
    float maxDuckSpeed = 20f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }

    }

    public void FixedUpdate()
    {

        if (mouseDown)
        {
            float deg = AngleBetween(mousePos, transform);

            if (Mathf.Abs(deg) > turnRate * Time.fixedDeltaTime)
            {
                body.MoveRotation(body.rotation + (turnRate * Mathf.Sign(deg)) * Time.fixedDeltaTime);
            }
            else
            {
                body.MoveRotation(body.rotation + deg);
            }

            body.AddRelativeForce(Vector2.up * Time.fixedDeltaTime * duckSpeed);


        }
        else {
            body.MoveRotation(body.rotation);
        }


        if (body.velocity.magnitude > maxDuckSpeed)
        {
            body.velocity = body.velocity.normalized * maxDuckSpeed;
        }

    }

    public static float AngleBetween(Vector3 look, Transform pos)
    {
        Vector3 enenmyToPlayer = (look) - pos.position;
        float angle = Vector2.Angle(enenmyToPlayer, pos.up);
        Vector3 cross = Vector3.Cross(enenmyToPlayer, pos.up);
        if (cross.z > 0)
            angle = -angle;
        return angle;
    }
}


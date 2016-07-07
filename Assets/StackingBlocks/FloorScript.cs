using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour
{

    [SerializeField]
    StackingGameLogic sgl;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        BlockScript bs = collision.gameObject.GetComponent<BlockScript>();
        if(bs != null){
            if (!bs.canTouchGround)
                sgl.lost();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGround;
    public PlayerController player;

    public MovGround_X mov;
    public Vector3 movp;

    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
       // mov = GameObject.FindGameObjectWithTag("movground").GetComponent<MovGround_X>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger == false)
        {
            player.grounded = true;
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false && collision.CompareTag("movground"))
        {
            movp = player.transform.position;
            movp.x += mov.speed * 1f;
            player.transform.position = movp;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            player.grounded = false;
        }

    }
}

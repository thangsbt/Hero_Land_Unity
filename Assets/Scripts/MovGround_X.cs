using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovGround_X : MonoBehaviour
{
    public float speed = 0.05f, changedirection = -1f;
    [SerializeField] Vector3 move;
    public PauseUI pausep;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        move = this.transform.position;
        pausep = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PauseUI>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if( GameController.instance.isGameOver == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (pausep.pause)
            {
                this.transform.position = this.transform.position;
            }
            if (pausep.pause == false)
            {
                move.x += speed;
                transform.position = move;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.isTrigger == false)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                speed *= changedirection;
            }
            
        }
    }
}

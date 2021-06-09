using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    public PauseUI pausep;
    public float timedelay;
    [SerializeField] float speed , changedirection = -1 ;
    [SerializeField] Vector2 move;

    private float intTimer;

    public PlayerController player;
    [SerializeField] float damage = 1f;

    private void Awake()
    {
        intTimer = Time.deltaTime;
        intTimer = timedelay;
        
    }
    void Start()
    {
        move = this.transform.position;
        pausep = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PauseUI>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isGameOver == true)
        {
            Time.timeScale = 0;
            
            this.transform.position = this.transform.position;
        }
        else
        {
            if (pausep.pause)
            {
                this.transform.position = this.transform.position;
            }
            if (pausep.pause == false)
            {
                move.y += speed;
                this.transform.position = move;
            }
        }
        
        //TimeDown();
    }
    //void TimeDown()
    //{
    //    timedelay -= Time.deltaTime;
    //    if(timedelay <= 0)
    //    {
    //        speed *= changedirection;
    //        timedelay = intTimer;
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.isTrigger == false)
        {
            speed *= changedirection;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player.SendMessageUpwards("Damage", damage);
            //player.Knocback(350f, transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SendMessageUpwards("Damage", damage);
            //player.Knocback(350f, transform.position);
        }
    }
}

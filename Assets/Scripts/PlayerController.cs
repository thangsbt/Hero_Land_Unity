using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public shieldTrigger shield;

    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigd;
    //[SerializeField] GroundCheck groundcheck;

    [SerializeField] float Speed = 100f , MaxSpeed = 3f , currentSpeed = 0f;
    [SerializeField] float JumPow = 400f, MaxJump = 5f, JumpForce = 7.5f;
    [SerializeField] bool  faceRight = true   , DoubleJump;
    [SerializeField] float h = 0;
    public bool grounded = true;

    private int jumparam = Animator.StringToHash("Jump");
    private int isgroundparam = Animator.StringToHash("isGround");

    public int ourhealth;
    public int maxhealth = 5;
    public float timeDead;
    //[SerializeField] float Knockpow;
    //[SerializeField] Vector2 Knockdir;

    // Start is called before the first frame update
    void Start()
    {
        shield = GameObject.FindGameObjectWithTag("shield").GetComponent<shieldTrigger>();
        rigd = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        

        ourhealth = maxhealth;
    }
    void Update()
    {
        anim.SetFloat("AirSpeedY", rigd.velocity.y);
        anim.SetBool(isgroundparam, grounded);
        anim.SetFloat("Speed", Mathf.Abs(rigd.velocity.x));
        if (GameController.instance.isWin == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    anim.SetTrigger(jumparam);
                    grounded = false;
                    rigd.AddForce(Vector2.up * JumPow);
                    //rigd.velocity = new Vector2(rigd.velocity.x, JumpForce);
                    DoubleJump = true;
                }
                else if (DoubleJump)
                {
                    DoubleJump = false;
                    //rigd.velocity = new Vector2(rigd.velocity.x,JumpForce * 0.8f);
                    anim.SetTrigger(jumparam);
                    rigd.velocity = new Vector2(rigd.velocity.x, 0);
                    rigd.AddForce((Vector2.up * JumPow * 0.8f));
                }


            }
        }
    }


    void FixedUpdate()
    {
        if(GameController.instance.isWin == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            float h = Input.GetAxis("Horizontal");
            rigd.AddForce((Vector2.right) * Speed * h);

            if (rigd.velocity.x > MaxSpeed)
            {
                rigd.velocity = new Vector2(MaxSpeed, rigd.velocity.y);
            }
            if (rigd.velocity.x < -MaxSpeed)
            {
                rigd.velocity = new Vector2(-MaxSpeed, rigd.velocity.y);
            }
            //if (rigd.velocity.y > MaxJump)
            //{
            //    rigd.velocity = new Vector2(rigd.velocity.x, MaxJump);
            //}

            if (grounded)
            {
                rigd.velocity = new Vector2(rigd.velocity.x * 0.7f, rigd.velocity.y);
            }
            // xoay mat
            if (h > 0 && !faceRight)
            {
                Flip();
            }
            if (h < 0 && faceRight)
            {
                Flip();
            }

            //hp
            //if (ourhealth <= 0)
            //{
            //    //gameObject.GetComponent<Animation>().Play("Player_Death");
            //    anim.SetBool("Death", true);
            //    StartCoroutine(Dead());
            //    //death();

            //}
        }
    }
    private void death()
    {
        if(PlayerPrefs.GetInt("BestScore") < GameController.instance.score)
        {
            PlayerPrefs.SetInt("BestScore", GameController.instance.score);
        }
        GameController.instance.GameOver();
        Time.timeScale = 0;
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(timeDead);
        death();
        yield return 0;
    }
    public void Damage(int damage)
    {
        if(shield.col.enabled == true)
        {
            ourhealth -= damage*0;
        }else if(shield.col.enabled == false)
        {
            ourhealth -= damage;
            // gameObject.GetComponent<Animation>().Play("red_Flash");
            gameObject.GetComponent<Animation>().Play("Player_Red_Flash Animation");
        }
        
        if (ourhealth <= 0)
        {

            //gameObject.GetComponent<Animation>().Play("Player_Death");
            anim.SetBool("Death", true);
            StartCoroutine(Dead());
            //death();

        }

    }
    public void Flip()
    {
        faceRight = !faceRight;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("princess"))
        {
            savescore();
            GameController.instance.YouWin();
            Time.timeScale = 0;

        }
        if (col.CompareTag("heart"))
        {

            Destroy(col.gameObject);
            ourhealth = 5;

        }
    }
    void savescore()
    {
        PlayerPrefs.SetInt("score", GameController.instance.score);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //    if (collision.gameObject.CompareTag("Enermy"))
    //    {
    //        Knocback(100f, transform.position);
    //    }
    //}
    //public void Knocback(float Knockpow, Vector2 Knockdir)
    //{
    //    rigd.velocity = new Vector2(0, 0);
    //    rigd.AddForce(new Vector2( Knockdir.x *  Knockpow, transform.position.y));

    //}

}

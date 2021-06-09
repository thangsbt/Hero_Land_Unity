using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Boss : MonoBehaviour
{
    public GameObject effect;
    private float distance;

    
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigd;
    
    public float FireIntTime;
    public float FireTimer;
    public float fireSpeed;

    public Transform target;
    [SerializeField] Transform fire_left;
    [SerializeField] Transform fire_right;
    public float curHealth;
    public float maxhealth = 300;

    [SerializeField] GameObject Fire;
    public Hearth_Dragon health_Dragon;
    [SerializeField] GameObject Tri_Dra_left;
    [SerializeField] GameObject Tri_Dra_right;
    [SerializeField] GameObject fireleft;
    [SerializeField] GameObject fireright;
    [SerializeField] GameObject health;
    [SerializeField] Collider2D triger;
    // public float damage;


    // Start is called before the first frame update
    private void Awake()
    {
        RangeCheck();
    }
    void Start()
    {
       
        anim = GetComponent<Animator>();
        rigd = GetComponent<Rigidbody2D>();
        //health_Dragon.LoseHealth(curHealth, damage);
        curHealth = maxhealth;
      
    }

    // Update is called once per frame
    void Update()
    {

        RangeCheck();
       
    }
    public void RangeCheck()
    {
        distance = Vector2.Distance(transform.position, target.position);
        flip();
        
    }
    public void FireAttack(bool Attackright)
    {
        FireTimer += Time.deltaTime;
        if(FireTimer >= FireIntTime)
        {

            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            if (!Attackright)
            {
                anim.SetBool("Dragon_Attack", true);
                GameObject fireCone = Instantiate(Fire, fire_left.position,
                    fire_left.rotation) as GameObject;

                fireCone.GetComponent<Rigidbody2D>().velocity = direction * fireSpeed;

                FireTimer = 0;
             
                StartCoroutine(TimerAnim(0.5f));

            }
            else
            {
                anim.SetBool("Dragon_Attack", true);
                GameObject fireCone = Instantiate(Fire, fire_right.position, 
                    fire_right.rotation) as GameObject;

                fireCone.GetComponent<Rigidbody2D>().velocity = direction * fireSpeed;
                FireTimer = 0;
              
                StartCoroutine(TimerAnim(0.5f));
            }
            
        }
       
    }

    public void flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x  )
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }
        transform.eulerAngles = rotation;
    }
    IEnumerator TimerAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("Dragon_Attack", false);
        yield return 0;
    }
    public void Damage(int damage)
    {
        curHealth -= damage;

        health_Dragon.LoseHealth(curHealth, damage);

        gameObject.GetComponent<Animation>().Play("red_Flash");
        if (curHealth < 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Tri_Dra_left.SetActive(false);
            Tri_Dra_right.SetActive(false);
            fireleft.SetActive(false);
            fireright.SetActive(false);
            health.SetActive(false);
            triger.enabled = false;
            anim.SetBool("Dead", true);
            
            //Destroy(gameObject);
        }

    }

}

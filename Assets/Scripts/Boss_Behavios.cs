using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss_Behavios : MonoBehaviour
{
    //public Transform raycast;
    //public LayerMask raycastMask;
    [SerializeField]
    public float raycastLenghth;
    public float attackDistance; // khong cach toi thieu de tan cong
    public float MoveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector]public bool inRange; // check pham vi player
    public GameObject hotzone;
    public GameObject trigerArea;
    //private RaycastHit2D hit;

    private Animator anim;
    private float distance; // store the distance between enemy and player 
    private bool attackMode;
    
    private bool cooling;// check if enemy is cooling after attack
    private float intTimer;

    public float healths;
    public float maxhealts = 200f;
    public HealthBarBoss healthbar;
    public float timeDead;

    public GameObject effect;
    
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject boxCollider;
    [SerializeField] GameObject Hit_box;
    [SerializeField] GameObject triger_area;
   // [SerializeField] GameObject hot_Zone;
    [SerializeField] GameObject Health;

    // Start is called before the first frame update
    void Start()
    {

        healths = maxhealts;
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Awake()
    {
        healths = maxhealts;
        SelectTarget();
        intTimer = timer; //  store the inital value of time
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack"))
        {
            SelectTarget();
        }
        //if (inRange)
        //{
        //    // thong so 1 : vi tris ban
        //    // thong so 2 : huong ban
        //    // thong so 3 : khoang cach de ban
        //    // thong so 4 la layer de chi phat hien doi tuong tren lop da chon
        //    hit = Physics2D.Raycast(raycast.position, 
        //                            transform.right, raycastLenghth, raycastMask);
        //    RaycastDebugger();
            
        //}

        // when player is detected
        //if(hit.collider != null)
        //    // kiem tra raycast xem co phai ko neu dung
        //{
        //    Bosslogic();
        //}
        //else if(hit.collider == null)
        //    // neu = null nghia la ko trong pham vi
        //{
        //    inRange = false;
        //}

        if(inRange)
        {
            //khi player ko trong pham vi stop di bo cua boss
            // anim.SetBool("canWalk", false);
            //StopAttack();

            Bosslogic();
        }
    }

    private void Bosslogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance > distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            cooldown();
            anim.SetBool("Attack", false);
        }
    }
    
    private void Attack()
    {
        timer = intTimer; //reset timer when player enter attack range
        attackMode = true;// to check if enemy can still attack or not
        //để kiểm tra xem kẻ thù có thể tấn công hay không

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    public void TriggerCooling()
    {
        cooling = true;
    }
    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }
  
    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    private void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack"))
        // truoc khi di chuyen, ta dam bao rang no khong phat hoat anh tan cong
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);


            // T/s1 : vi tri hien tai enemy T/s2: vi tri muc tieu T/s3: toc do
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
        }
    }
    public void SelectTarget()
    {
        float distanceToleft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToright = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToleft > distanceToright)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        flip();
    }

    public void flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }
    //private void RaycastDebugger()
    //{
    //   if(distance > attackDistance)
    //        // khoang cach giua player and enemy > khoang cach tan cong ta co dong lenh duoi
    //    {
    //        Debug.DrawRay(raycast.position, transform.right * raycastLenghth, Color.red);
    //    }
    //   else if( attackDistance > distance)
    //    {
    //        Debug.DrawRay(raycast.position,transform.right * raycastLenghth, Color.green);
    //    }
    //}



    //private void OnTriggerEnter2D(Collider2D trig)
    //{
    //    if (trig.gameObject.tag=="Player")
    //    {
    //        target = trig.transform;
    //        inRange = true;
    //        flip();
    //    }
    //}

    public void Damage(int damage)
    {
        healths -= damage;
        healthbar.LoseHealth(healths, damage);
        gameObject.GetComponent<Animation>().Play("red_Flash");
        if (healths <= 0)
        {
            anim.SetBool("Dead", true);
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(timeDead);
        //Destroy(gameObject);
        boxCollider.SetActive (false);
        triger_area.SetActive(false);
        Hit_box.SetActive(false);
        hotzone.SetActive(false);
        Health.SetActive(false);
        rb.bodyType = RigidbodyType2D.Static;
        Instantiate(effect, transform.position, Quaternion.identity);
        yield return 0;
    }
    
}

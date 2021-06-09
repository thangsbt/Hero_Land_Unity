using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enermy : MonoBehaviour
{
    
    public Transform target;
    //reference to waypoints
    public List<Transform> point_enemy;

    //the int value for next point index; 
    public int NextId_enemy = 0;
    //the value of that applies to ID for changing
    int idchangevalue = 1;
    //Speed of movement or flying 
    public float speed = 2;

    [SerializeField] float curHealth = 100;
    public int dmg = 1;
    [SerializeField] Rigidbody2D rigd;
    [SerializeField] bool pains = false;
    [SerializeField] Animator anim;

   

    public GameObject effect;
   
    void Start()
    {
         rigd = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Reset()
    {
        Init();
    }
    void Init()
    {
        //Make box collider trigger
        GetComponent<BoxCollider2D>().isTrigger = true;

        //creat Root object
        GameObject root = new GameObject(name + "_Root");

        //reset position of root to this enemy object
        root.transform.position = transform.position;

        // set enemy object as child of root
        transform.SetParent(root.transform);

        //Creat Waypoints object
        GameObject waypoints = new GameObject("Waypoints");

        //Reset Waypoints position to root
        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        //create two points (gameobject) and reset their position to wyapoint object
        //Make the Points children of waypoints object
        GameObject p_1 = new GameObject("Point_1");
        p_1.transform.SetParent(waypoints.transform);
        p_1.transform.position = root.transform.position;

        GameObject p_2 = new GameObject("Point_2");
        p_2.transform.SetParent(waypoints.transform);
        p_2.transform.position = root.transform.position;

        //Init points list then add the points to it
        point_enemy = new List<Transform>();
        point_enemy.Add(p_1.transform);
        point_enemy.Add(p_2.transform);
    }

    void Update()
    {
        
        anim.SetBool("Pain", pains);
        
        
        if(pains == true)
        {
            //transform.position = this.transform.position;
            if (target.transform.position.x > this.transform.position.x)
            {
                rigd.velocity = new Vector2(0, 0);
                rigd.AddForce(Vector2.left * 4f,ForceMode2D.Impulse);

                // transform.position = new Vector2(transform.position.x * (-0.00001f) * Time.deltaTime, transform.position.y);
            }
            if (transform.position.x > target.position.x)
            {
                rigd.velocity = new Vector2(0, 0);
                rigd.AddForce(Vector2.right * 4f,ForceMode2D.Impulse);
                //transform.position =  this.transform.position;
                //rigd.velocity = new Vector2(0, 0);
                //rigd.AddForce(new Vector2(transform.position.x * 50f, transform.position.y));
                //transform.position = new Vector2(transform.position.x * (10f) * Time.deltaTime, transform.position.y); ;
            }
        }
        else
        {
            //rigd.velocity = new Vector2(transform.localScale.x, 0) * speed;
            MoveToNextPoint();
        }

    }
    void MoveToNextPoint()
    {
        //get the next Point transform  
        Transform goalPoint = point_enemy[NextId_enemy];
        //Flip the enemy transform to look into the point direction
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //MOve the enemy toWards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        // check the distance between enemy and goal point to trigger next point 
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            //check if we are at the end of the line (make the change -1)
            //2 point   (0,1) next == point.count(2)-1
            if (NextId_enemy == point_enemy.Count - 1)
            {
                idchangevalue = -1;
            }
            //check if we are at the start of the line (make the change +1)
            if (NextId_enemy == 0)
            {
                idchangevalue = 1;
            }
            //apply the change on the NextID
            NextId_enemy += idchangevalue;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AttackTrigger"))
        {
            pains = true;
        }
        if (collision.collider.isTrigger == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessageUpwards("Damage", dmg);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackTrigger"))
        {
                pains = true;
        }
        if (collision.isTrigger == false)
        {
            
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessageUpwards("Damage", dmg);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackTrigger"))
        {
            pains = false;
        }
    }
    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("red_Flash");
        if (curHealth <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    

}

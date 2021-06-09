using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Opossum : MonoBehaviour
{
    public Transform target;
    [SerializeField] float curHealth = 100;
    public int dmg = 1;
    public GameObject effect;
    [SerializeField] bool pain;

    //reference to waypoints
    public List<Transform> point;

    //the int value for next point index; 
    public int NextId = 0;
    //the value of that applies to ID for changing
    int idchangevalue = 1;
    //Speed of movement or flying 
    public float speed = 2;
    [SerializeField] Rigidbody2D rigd;
     void Start()
    {
        rigd = gameObject.GetComponent<Rigidbody2D>();
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
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;

        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        //Init points list then add the points to it
        point = new List<Transform>();
        point.Add(p1.transform);
        point.Add(p2.transform);
    }

    private void Update()
    {
        if (pain == true)
        {
            if (target.transform.position.x > this.transform.position.x)
            {
                rigd.velocity = new Vector2(0, 0);
                rigd.AddForce(Vector2.left * 4f, ForceMode2D.Impulse);

                // transform.position = new Vector2(transform.position.x * (-0.00001f) * Time.deltaTime, transform.position.y);
            }
            if (transform.position.x > target.position.x)
            {
                rigd.velocity = new Vector2(0, 0);
                rigd.AddForce(Vector2.right * 4f, ForceMode2D.Impulse);
                
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
        Transform goalPoint = point[NextId];
        //Flip the enemy transform to look into the point direction
        if(goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //MOve the enemy toWards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed*Time.deltaTime);

        // check the distance between enemy and goal point to trigger next point 
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            //check if we are at the end of the line (make the change -1)
            //2 point   (0,1) next == point.count(2)-1
            if(NextId == point.Count - 1)
            {
                idchangevalue = -1;
            }
            //check if we are at the start of the line (make the change +1)
            if (NextId == 0)
            {
                idchangevalue = 1;
            }
            //apply the change on the NextID
            NextId += idchangevalue;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackTrigger"))
        {
            pain = true;

            
        }
        if (collision.isTrigger == false)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessageUpwards("Damage", dmg);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.isTrigger == false)
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
            pain = false;
        }
    }

}

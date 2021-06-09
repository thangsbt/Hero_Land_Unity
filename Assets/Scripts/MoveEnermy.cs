using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnermy : MonoBehaviour
{
    public bool canMove = false;
    public float speed = 4f;
    [SerializeField] Rigidbody2D rigd;
    public Enermy enermy;
    [SerializeField] bool pains;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigd = gameObject.GetComponent<Rigidbody2D>();
        enermy = GameObject.FindGameObjectWithTag("Enermy").GetComponent<Enermy>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = 
            new Vector2(transform.localScale.x, 0) * speed;
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canMove = collision.gameObject.tag == ("Ground");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movground : MonoBehaviour
{
    [SerializeField] float speed = 0.2f ,  changedirection = -1f;
    [SerializeField] Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        move = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move.y += speed;
        this.transform.position = move;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wallground"))
        {
            speed *= changedirection;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Dragon : MonoBehaviour
{
    public PlayerController player;
    public float lifetime = 2;
    public GameObject explor;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger == false)
        {
            if (col.CompareTag("Player"))
            {
                col.SendMessageUpwards("Damage", 1);
                Destroy(gameObject);
            }
            
        }

    }
    private void OnDestroy()
    {
        GameObject ex = Instantiate(explor, transform.position, Quaternion.identity) as GameObject;
        Destroy(ex, 0.3f);
    }
}

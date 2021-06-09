using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_damage : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    public int damage = 1;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SendMessageUpwards("Damage", damage);
            
        }
    }
}

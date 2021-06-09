using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_trap : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] float damage = 5f;
    // Start is called before the first frame update
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
            //player.Knocback(350f, transform.position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.SendMessageUpwards("Damage", damage);
            //  player.Knocback(350f, transform.position);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldTrigger : MonoBehaviour
{
    public PlayerController player;
    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

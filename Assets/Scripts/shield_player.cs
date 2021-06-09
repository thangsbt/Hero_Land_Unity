using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_player : MonoBehaviour
{
    [SerializeField] Collider2D trigger;
    [SerializeField] Animator anim;
    [SerializeField] float shielddelay = 0.5f;
    public bool shielding = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && !shielding)
        {
            shielding = true;
            trigger.enabled = true;
            
            shielddelay = 0.5f;
        }
        if (shielding)
        {
            if(shielddelay > 0)
            {
                shielddelay -= Time.deltaTime;
            }
            else
            {
                shielding = false;
                trigger.enabled = false;
            }
        }
        anim.SetBool("shield", shielding);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    
    [SerializeField] Animator anim;
    [SerializeField] Collider2D trigger;

    [SerializeField] float AttackDeplay = 0.3f;
    [SerializeField] bool Attacking = false;
    [SerializeField] float currentAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.isGameOver == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z) && !Attacking)
            {
                currentAttack++;

                Attacking = true;
                trigger.enabled = true;
                AttackDeplay = 0.3f;

                if (currentAttack > 3)
                    currentAttack = 1;
                anim.SetTrigger("Attack" + currentAttack);
            }
            if (Attacking)
            {
                if (AttackDeplay > 0)
                {
                    AttackDeplay -= Time.deltaTime;
                }
                else
                {
                    Attacking = false;
                    trigger.enabled = false;
                }
            }
            anim.SetBool("Attacking", Attacking);
        }
    }
}

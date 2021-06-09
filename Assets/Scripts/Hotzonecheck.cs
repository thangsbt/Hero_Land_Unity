using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotzonecheck : MonoBehaviour
{
    private Boss_Behavios bossparent;

    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        bossparent = GetComponentInParent<Boss_Behavios>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Attack"))
        {
            bossparent.flip();
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if(bossparent.healths <= 0)
            {
                gameObject.SetActive(false);
                bossparent.trigerArea.SetActive(false);
            }
            else
            {
                inRange = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            bossparent.trigerArea.SetActive(true);
            bossparent.inRange = false;
            bossparent.SelectTarget();
        }
    }
}

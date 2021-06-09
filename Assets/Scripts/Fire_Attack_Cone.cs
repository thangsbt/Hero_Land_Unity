using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Attack_Cone : MonoBehaviour
{
    public Dragon_Boss dragon;
    public bool isLeft =  false;
    

    private void Awake()
    {
        dragon = gameObject.GetComponentInParent<Dragon_Boss>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                dragon.FireAttack(false);
              
                
            }
            else
            {
                dragon.FireAttack(true);
                
            }
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (isLeft)
    //        {
                
    //        }
    //        else
    //        {
    //            dragon.FireAttack(true);
    //        }
    //    }
    //}
}

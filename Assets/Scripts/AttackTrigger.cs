using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public Enermy enermy;
    public Boss_Behavios boss;
    public int dmg = 20;
    public Animator anim;
    


    // Start is called before the first frame update
    void Start()
    {
        enermy = GameObject.FindGameObjectWithTag("Enermy").GetComponent<Enermy>();
        //boss = GameObject.FindGameObjectWithTag("boss").GetComponent<Boss_Behavios>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enermy") || collision.CompareTag("boss"))
        {
            collision.SendMessageUpwards("Damage", dmg);
            
           
        }
       

    }
  
   
}

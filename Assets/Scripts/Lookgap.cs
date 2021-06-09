using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookgap : MonoBehaviour
{
    public GameObject Enermy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            Enermy.transform.localScale = new Vector3(-Enermy.transform.localScale.x,
                Enermy.transform.localScale.y,
                Enermy.transform.localScale.z);
        }
        
    }
}

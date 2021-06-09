using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int cointValue = 100;
    public GameObject effect_coin;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameController.instance.Addscore(cointValue);

            Destroy(gameObject);
        }
       
    }
    private void OnDestroy()
    {
        GameObject Eff = Instantiate(effect_coin, transform.position, Quaternion.identity) as GameObject;
        Destroy(Eff, 0.3f);
    }

}

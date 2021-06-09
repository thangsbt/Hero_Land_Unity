using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearth_Dragon : MonoBehaviour
{
    public Image fillBar;
    public Dragon_Boss dragon;
    //public int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoseHealth(float health,float value)
    {
        health -= value;
        fillBar.fillAmount = health / dragon.maxhealth;

      
    }
    // Update is called once per frame
    void Update()
    {
        //slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}

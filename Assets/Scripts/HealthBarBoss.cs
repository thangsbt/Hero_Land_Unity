using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{

    public Image fillBar;
    public Boss_Behavios boss;

    public void LoseHealth(float health, float value)
    {
        health -= value;
        fillBar.fillAmount = health /boss.maxhealts;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}

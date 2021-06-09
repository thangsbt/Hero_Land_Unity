using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Root : MonoBehaviour
{
    public Transform target;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
     void Awake()
    {
        RangeCheck();   
    }

    // Update is called once per frame
    void Update()
    {
        RangeCheck();
    }
    public void RangeCheck()
    {
        distance = Vector2.Distance(transform.position, target.position);
        flip();
    }
    public void flip()
    {
        Vector2 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 0;
        }
        else
        {
            rotation.y = 180;
        }
        transform.eulerAngles = rotation;
    }


}

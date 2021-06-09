using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreacheck : MonoBehaviour
{
    private Boss_Behavios bossparent;

    private void Awake()
    {
        bossparent = GetComponentInParent<Boss_Behavios>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bossparent.healths <= 0)
            {
                bossparent.hotzone.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
                bossparent.target = collision.transform;
                bossparent.inRange = true;
                bossparent.hotzone.SetActive(true);
            }
        }
    }
}
